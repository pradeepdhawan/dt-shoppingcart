namespace ShoppingCart.DiscountLib.DiscountCalculator
{
    using System;
    using ShoppingCart.DiscountLib.CustomException;
    using ShoppingCart.DiscountLib.Model;

    public class AbsoluteDiscountCalculator : AbstractDiscountCalculator
    {
        public AbsoluteDiscountCalculator(ICart cart, IDiscountParameters discount)
        {
            this.Cart = cart;
            this.Discount = discount;
            this.Validate();
            this.discount = this.GetDiscount();
        }

        private readonly double discount;
        private bool validated = false;

        private double GetDiscount()
        {
            this.Validate();
            return this.Discount.DiscountFactor;
        }

        private void Validate()
        {
            if (this.validated)
            {
                return;
            }

            if (this.Cart == null || this.Cart.CartItems == null || this.Cart.CartItems.Count == 0)
            {
                throw new InvalidInputException("Absolute discount cannot be calculated on empty cart", "Cart");
            }

            if (this.Cart.Cost() < this.Discount.DiscountFactor)
            {
                throw new InvalidInputException("Absolute discount must be less than cart value", "Discount");
            }

            if (this.Discount == null || this.Discount.DiscountFactor <= 0)
            {
                throw new InvalidInputException("Absolute discount must be set and greater than 0", "Discount");
            }

            this.validated = true;
        }

        public override double CalculateDiscount()
        {
            this.Validate();
            return this.discount;
        }

        public override double DiscountWeightage()
        {
            this.Validate();
            return this.CalculateDiscount() * 100 / this.Cart.Cost();
        }
    }
}
