namespace ShoppingCart.DiscountLib.Model
{
    public interface ICartItem
    {
        IProduct Product { get; }

        uint Quantity { get; }

        double Price();

        void Increment(uint quantity);

        void Decrement(uint quantity);
    }
}
