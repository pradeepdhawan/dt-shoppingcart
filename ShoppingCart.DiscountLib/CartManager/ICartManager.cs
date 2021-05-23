namespace ShoppingCart.DiscountLib.CartManager
{
    using System.Collections.Generic;
    using ShoppingCart.DiscountLib.DiscountCalculator;

    public interface ICartManager
    {
        abstract void Build();

        abstract double Evaluate(Model.CartEvaluationAlgorithm algorithm, out List<IDiscountParameters> successfullDiscounts, out double originalCartValue);
    }
}
