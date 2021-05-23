namespace ShoppingCart.DiscountCalculatorShoppingCart.DiscountLib.DiscountCalculatorFactory
{
    using ShoppingCart.DiscountLib.DiscountCalculator;
    using ShoppingCart.DiscountLib.Model;

    public interface IDiscountCalculatorFactory
    {
        IDiscountCalculator Create(ICart cart, IDiscountParameters discount);
    }
}