namespace ShoppingCart.DiscountLib.DiscountCalculator
{
    using System.Collections.Generic;
    using ShoppingCart.DiscountLib.Model;

    public interface IDiscountParameters
    {
        DiscountType DiscountType { get; }

        bool CanApplyRecursively { get; }

        List<ICartItem> DiscountCondition { get; }

        List<ICartItem> DiscountCalculatedOnItems { get; }

        double DiscountFactor { get; }

        abstract List<ICartItem> MergedCartItems { get; }
    }
}
