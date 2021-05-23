namespace ShoppingCart.DiscountLib.Model
{
    using System.Collections.Generic;

    public interface ICart
    {
        int Id { get; }

        List<ICartItem> CartItems { get; }

        abstract void Add(ICartItem cartItem);

        abstract void Remove(ICartItem cartItem);

        abstract double Cost();
    }
}
