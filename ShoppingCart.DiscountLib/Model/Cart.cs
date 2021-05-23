namespace ShoppingCart.DiscountLib.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ShoppingCart.DiscountLib.CustomException;

    public class Cart : ICart
    {
        public int Id { get; private set; }

        public List<ICartItem> CartItems { get; }

        public Cart()
        {
            this.CartItems = new List<ICartItem>();
        }

        public Cart(int id, List<ICartItem> cartItems)
        {
            this.Id = id;
            this.CartItems = cartItems;
        }

        public void Add(ICartItem cartItem)
        {
            var itemInCart = this.CartItems.First(o => o.Product.Id == cartItem.Product.Id);
            if (itemInCart == null)
            {
                this.CartItems.Add(cartItem);
            }
            else
            {
                itemInCart.Increment(cartItem.Quantity);
            }
        }

        public void Remove(ICartItem cartItem)
        {
            var itemInCart = this.CartItems.FirstOrDefault(o => o.Product.Id == cartItem.Product.Id);
            if (itemInCart == null)
            {
                throw new InvalidInputException("Product is not in cart :" + cartItem.Product.Name, "CartItem");
            }
            else if (itemInCart.Quantity < cartItem.Quantity)
            {
                throw new InvalidInputException("Product is quantity in cart is : " + itemInCart.Quantity + " and required to remove is : " + cartItem.Quantity, "CartItem");
            }

            itemInCart.Decrement(cartItem.Quantity);
        }

        public double Cost()
        {
            double cost = 0;
            foreach (var cartItem in this.CartItems)
            {
                cost += cartItem.Price();
            }

            return cost;
        }
    }
}