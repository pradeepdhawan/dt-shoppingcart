namespace ShoppingCart.DiscountLib.DiscountCalculator
{
    using ShoppingCart.DiscountLib.Model;

    public abstract class AbstractDiscountCalculator : IDiscountCalculator
    {
        public ICart Cart { get; protected set; }

        public IDiscountParameters Discount { get; protected set; }

        public abstract double CalculateDiscount();

        public abstract double DiscountWeightage();
    }
}
