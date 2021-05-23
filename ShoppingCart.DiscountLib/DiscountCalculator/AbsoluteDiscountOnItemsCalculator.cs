namespace ShoppingCart.DiscountLib.DiscountCalculator
{
    using System;
    using ShoppingCart.DiscountLib.CustomException;
    using ShoppingCart.DiscountLib.Model;

    public class AbsoluteDiscountOnItemsCalculator : AbstractDiscountCalculator
    {
        public AbsoluteDiscountOnItemsCalculator(ICart cart, IDiscountParameters discount)
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
                throw new InvalidInputException("Absolute discount on specific items cannot be calculated on empty cart", "Cart");
            }

            if (this.Cart.Cost() < this.Discount.DiscountFactor)
            {
                throw new InvalidInputException("Absolute discount on specific  must be less than cart value", "Discount");
            }

            if (this.Discount == null || this.Discount.DiscountFactor <= 0)
            {
                throw new InvalidInputException("Absolute discount on specific must be set and greater than 0", "Discount");
            }

            if (this.Discount.DiscountCalculatedOnItems == null || this.Discount.DiscountCalculatedOnItems.Count <= 0)
            {
                throw new InvalidInputException("Absolute discount on specific must have some specific items", "Discount");
            }

            double valueOfItems = 0;
            foreach (var item in this.Discount.DiscountCalculatedOnItems)
            {
                valueOfItems += item.Price();
            }

            if (valueOfItems < this.Discount.DiscountFactor)
            {
                throw new InvalidInputException("Absolute discount on specific items is more than value of cart items:" + valueOfItems, "Discount");
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
            return this.CalculateDiscount() * 100 / this.Cart.Cost();
        }
    }
}