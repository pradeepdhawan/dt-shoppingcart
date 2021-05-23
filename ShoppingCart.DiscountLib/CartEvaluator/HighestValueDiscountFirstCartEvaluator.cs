namespace ShoppingCart.DiscountLib.CartEvaluator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ShoppingCart.DiscountCalculator.DiscountCalculatorFactory;
    using ShoppingCart.DiscountLib.DiscountCalculator;
    using ShoppingCart.DiscountLib.Model;

    public class HighestValueDiscountFirstCartEvaluator : CartEvaluator
    {
        public HighestValueDiscountFirstCartEvaluator(ICart cart, List<DiscountByWeightage> weightedDiscounts)
            : base()
        {
            this.Cart = cart;
            this.WeightedDiscounts = weightedDiscounts.OrderByDescending(o => o.Weightage).ToList();
        }

        public override double Evaluate(out List<IDiscountParameters> successfullDiscounts, out double originalCartValue)
        {
            successfullDiscounts = new List<IDiscountParameters>();
            originalCartValue = this.Cart.Cost();
            var newCartValue = this.Cart.Cost();

            var cloned = new List<ICartItem>();
            foreach (var item in this.Cart.CartItems)
            {
                cloned.Add(new CartItem(item.Product, item.Quantity));
            }

            var cart = new Cart(this.Cart.Id, cloned);

            foreach (var discount in this.WeightedDiscounts)
            {
                if (cart.Cost() > 0)
                {
                    var retry = true;
                    while (retry)
                    {
                        var cartItemsThatMakeUpCondition = discount.Discount.MergedCartItems;
                        var exists = cartItemsThatMakeUpCondition.All(x => cart.CartItems.Any(y => x.Product == y.Product && x.Quantity <= y.Quantity));
                        if (exists)
                        {
                            successfullDiscounts.Add(discount.Discount);
                            var discountCalculator = DiscountCalculatorFactory.Instance.Create(this.Cart, discount.Discount);
                            var currentDiscountValue = discountCalculator.CalculateDiscount();
                            newCartValue -= currentDiscountValue;
                            foreach (var item in cartItemsThatMakeUpCondition)
                            {
                                cart.Remove(item);
                            }
                        }

                        retry = exists && discount.Discount.CanApplyRecursively;
                    }
                }
            }

            return newCartValue;
        }
    }
}
