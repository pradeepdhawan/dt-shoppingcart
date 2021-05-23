namespace ShoppingCart.DiscountLib.Model
{
    using System;
    using ShoppingCart.DiscountLib.CustomException;

    public class CartItem : ICartItem, ICloneable
    {
        public IProduct Product { get; private set; }

        public uint Quantity { get; private set; }

        public CartItem(IProduct product, uint initialQuantity = 1)
        {
            this.Product = product;
            this.Quantity = initialQuantity;
        }

        public double Price()
        {
            return this.Product.Price * this.Quantity;
        }

        public void Increment(uint quantity)
        {
            if (quantity >= 1)
            {
                this.Quantity += quantity;
            }
            else
            {
                throw new InvalidInputException("Quantity of item is invalid:" + quantity, "Quantity");
            }
        }

        public void Decrement(uint quantity)
        {
            if (this.Quantity >= quantity)
            {
                this.Quantity -= quantity;
            }
            else
            {
                throw new InvalidInputException("Quantity of item in cart to be removed:" + quantity + " is more than expected:" + this.Quantity, "Quantity");
            }
        }

        public object Clone()
        {
            return new CartItem(new Product(this.Product.Id, this.Product.Name, this.Product.Price), this.Quantity);
        }
    }
}
