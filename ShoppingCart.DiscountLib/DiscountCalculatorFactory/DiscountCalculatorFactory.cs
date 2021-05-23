namespace ShoppingCart.DiscountCalculator.DiscountCalculatorFactory
{
    using System;
    using ShoppingCart.DiscountCalculatorShoppingCart.DiscountLib.DiscountCalculatorFactory;
    using ShoppingCart.DiscountLib.DiscountCalculator;
    using ShoppingCart.DiscountLib.Model;

    public class DiscountCalculatorFactory : IDiscountCalculatorFactory
    {
        public static DiscountCalculatorFactory Instance = new DiscountCalculatorFactory();

        private DiscountCalculatorFactory()
        {
        }

        public IDiscountCalculator Create(ICart cart, IDiscountParameters discountParameters)
        {
            IDiscountCalculator instance;
            switch (discountParameters.DiscountType)
            {
                case DiscountType.Absolute:
                    instance = new AbsoluteDiscountCalculator(cart, discountParameters);
                    break;
                case DiscountType.AbsoluteDiscountOnItems:
                    instance = new AbsoluteDiscountOnItemsCalculator(cart, discountParameters);
                    break;
                case DiscountType.Percentage:
                    instance = new PercentageDiscountCalculator(cart, discountParameters);
                    break;
                case DiscountType.PercentageDiscountOnItems:
                    instance = new PercentageDiscountOnItemsCalculator(cart, discountParameters);
                    break;
                case DiscountType.CheapestItemFree:
                default:
                    throw new NotImplementedException(discountParameters.DiscountType.ToString() + " factory missing");
            }

            return instance;
        }
    }
}