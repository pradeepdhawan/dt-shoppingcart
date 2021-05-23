namespace ShoppingCart.DiscountLib
{
    public interface IProduct
    {
        int Id { get; }

        string Name { get; }

        double Price { get; }
    }
}