namespace ShoppingCart.DiscountLib
{
    public class Product : IProduct
    {
        public int Id { get; }

        public string Name { get; }

        public double Price { get; }

        public Product(int id, string name, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }
    }
}
