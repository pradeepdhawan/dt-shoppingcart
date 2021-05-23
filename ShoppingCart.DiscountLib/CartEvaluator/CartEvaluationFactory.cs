namespace ShoppingCart.DiscountLib.CartEvaluator
{
    using System.Collections.Generic;

    public class CartEvaluationFactory
    {
        public static CartEvaluationFactory Instance = new CartEvaluationFactory();

        private CartEvaluationFactory()
        {
        }

        public ICartEvaluator Create(Model.CartEvaluationAlgorithm algorithm, Model.ICart cart, List<Model.DiscountByWeightage> weightedDiscounts)
        {
            ICartEvaluator instance;
            switch (algorithm)
            {
                case Model.CartEvaluationAlgorithm.HightestValueDiscountFirst:
                    instance = new HighestValueDiscountFirstCartEvaluator(cart, weightedDiscounts);
                    break;
                case Model.CartEvaluationAlgorithm.MaximumCombinedDiscount:
                case Model.CartEvaluationAlgorithm.LowestValueDiscountFirst:
                default:
                    throw new System.NotImplementedException(algorithm.ToString() + " factory missing");
            }

            return instance;
        }
    }
}