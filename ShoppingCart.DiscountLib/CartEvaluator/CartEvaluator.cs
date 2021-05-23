namespace ShoppingCart.DiscountLib.CartEvaluator
{
    using System.Collections.Generic;
    using ShoppingCart.DiscountLib.DiscountCalculator;
    using ShoppingCart.DiscountLib.Model;

    public abstract class CartEvaluator : ICartEvaluator
    {
        public ICart Cart { get; protected set; }

        public List<DiscountByWeightage> WeightedDiscounts { get; protected set; }

        public abstract double Evaluate(out List<IDiscountParameters> successfullDiscounts, out double originalCartValue);
    }
}
