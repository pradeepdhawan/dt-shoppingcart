namespace ShoppingCart.DiscountLib.DiscountCalculator
{
    using System;
    using ShoppingCart.DiscountLib.CustomException;
    using ShoppingCart.DiscountLib.Model;

    public class PercentageDiscountOnItemsCalculator : AbstractDiscountCalculator
    {
        public PercentageDiscountOnItemsCalculator(ICart cart, IDiscountParameters discount)
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
            return this.CostOfItems() * this.Discount.DiscountFactor / 100;
        }

        private void Validate()
        {
            if (this.validated)
            {
                return;
            }

            if (this.Cart == null || this.Cart.CartItems == null || this.Cart.CartItems.Count == 0)
            {
                throw new InvalidInputException("Percentage discount on specific items cannot be calculated on empty cart", "Cart");
            }

            if (this.Discount == null || this.Discount.DiscountFactor <= 0 || this.Discount.DiscountFactor > 100)
            {
                throw new InvalidInputException("Percentage discount on specific items must be a valid value between 0 and 100 and should not be null", "Discount");
            }

            if (this.Discount.DiscountCalculatedOnItems == null || this.Discount.DiscountCalculatedOnItems.Count <= 0)
            {
                throw new InvalidInputException("Percentage discount on specific must have some specific items", "Discount");
            }

            if (this.Discount.DiscountCalculatedOnItems == null || this.Discount.DiscountCalculatedOnItems.Count <= 0)
            {
                throw new InvalidInputException("Percentage discount on specific must have some specific items", "Discount");
            }
        }

        private double CostOfItems()
        {
            double valueOfItems = 0;
            foreach (var item in this.Discount.DiscountCalculatedOnItems)
            {
                valueOfItems += item.Price();
            }

            return valueOfItems;
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