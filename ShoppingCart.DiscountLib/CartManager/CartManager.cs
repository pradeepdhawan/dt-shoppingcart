namespace ShoppingCart.DiscountLib.CartManager
{
    using System.Collections.Generic;
    using System.Linq;
    using ShoppingCart.DiscountCalculator.DiscountCalculatorFactory;
    using ShoppingCart.DiscountLib.CartEvaluator;
    using ShoppingCart.DiscountLib.DiscountCalculator;
    using ShoppingCart.DiscountLib.Model;

    public class CartManager : ICartManager
    {
        public ICart Cart { get; private set; }

        public List<IDiscountParameters> Discounts { get; private set; }

        public List<DiscountByWeightage> WeightedDiscounts { get; private set; }

        public CartManager(ICart cart, List<IDiscountParameters> discounts)
        {
            this.Cart = cart;
            this.Discounts = discounts;
        }

        public void Build()
        {
            this.WeightedDiscounts = (from discount in this.Discounts
                                      let discountCalculator = DiscountCalculatorFactory.Instance.Create(this.Cart, discount)
                                      select new DiscountByWeightage(discountCalculator.DiscountWeightage(), discount)).ToList();
        }

        public double Evaluate(CartEvaluationAlgorithm algorithm, out List<IDiscountParameters> successfullDiscounts, out double originalCartValue)
        {
            ICartEvaluator evaluator = CartEvaluationFactory.Instance.Create(algorithm, this.Cart, this.WeightedDiscounts);
            var cartValueAfterDiscount = evaluator.Evaluate(out successfullDiscounts, out originalCartValue);
            return cartValueAfterDiscount;
        }
    }
}
