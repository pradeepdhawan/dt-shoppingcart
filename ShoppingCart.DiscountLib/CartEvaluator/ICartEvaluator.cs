namespace ShoppingCart.DiscountLib.CartEvaluator
{
    using System.Collections.Generic;
    using ShoppingCart.DiscountLib.DiscountCalculator;
    using ShoppingCart.DiscountLib.Model;

    public interface ICartEvaluator
    {
        ICart Cart { get; }

        List<DiscountByWeightage> WeightedDiscounts { get; }

        abstract double Evaluate(out List<IDiscountParameters> successfullDiscounts, out double originalCartValue);
    }
}
