
namespace ShoppingCart.DiscountLib.Model
{
    using ShoppingCart.DiscountLib.DiscountCalculator;

    public interface IDiscountCalculator
    {
        ICart Cart { get; }

        IDiscountParameters Discount { get; }

        abstract double CalculateDiscount();

        abstract double DiscountWeightage();
    }
}