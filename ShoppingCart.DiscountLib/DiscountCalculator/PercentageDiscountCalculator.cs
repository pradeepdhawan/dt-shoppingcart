namespace ShoppingCart.DiscountLib.DiscountCalculator
{
    using System;
    using ShoppingCart.DiscountLib.CustomException;
    using ShoppingCart.DiscountLib.Model;

    public class PercentageDiscountCalculator : AbstractDiscountCalculator
    {
        public PercentageDiscountCalculator(ICart cart, IDiscountParameters discount)
        {
            this.Cart = cart;
            this.Discount = discount;
            this.Validate();
            this.validated = true;
            this.discount = this.GetDiscount();
        }

        private readonly double discount;
        private readonly bool validated = false;

        private double GetDiscount()
        {
            this.Validate();
            return this.Cart.Cost() * this.Discount.DiscountFactor / 100;
        }

        private void Validate()
        {
            if (this.validated)
            {
                return;
            }

            if (this.Cart == null || this.Cart.CartItems == null || this.Cart.CartItems.Count == 0)
            {
                throw new InvalidInputException("Percentage discount cannot be calculated on empty cart", "Cart");
            }

            if (this.Discount == null || this.Discount.DiscountFactor <= 0 || this.Discount.DiscountFactor > 100)
            {
                throw new InvalidInputException("Percentage discount must be a valid value between 0 and 100 and should not be null", "Discount");
            }
        }

        public override double CalculateDiscount()
        {
            this.Validate();
            return this.discount;
        }

        public override double DiscountWeightage()
        {
            this.Validate();
            return this.Discount.DiscountFactor;
        }
    }
}