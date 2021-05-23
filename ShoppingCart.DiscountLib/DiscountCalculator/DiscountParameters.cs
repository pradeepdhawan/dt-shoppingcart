namespace ShoppingCart.DiscountLib.DiscountCalculator
{
    using System.Collections.Generic;
    using System.Linq;
    using ShoppingCart.DiscountLib.Model;

    public class DiscountParameters : IDiscountParameters
    {
        public DiscountParameters(DiscountType discountType, List<ICartItem> discountCondition, List<ICartItem> discountCalculatedOnItems, double discountFactor)
        {
            this.DiscountType = discountType;
            this.DiscountCondition = discountCondition;
            this.DiscountCalculatedOnItems = discountCalculatedOnItems;
            this.DiscountFactor = discountFactor;
            this.Validate();
        }

        private bool validated = false;

        private void Validate()
        {
            if (this.validated)
            {
                return;
            }

            if (this.DiscountFactor <= 0.0)
            {
                throw new System.ArgumentException("Discount cannot be negative", "Discount");
            }

            if (this.DiscountCondition == null || this.DiscountCondition.Count == 0)
            {
                throw new System.ArgumentException("Discount condition cannot be empty", "Discount");
            }

            this.validated = true;
        }

        public DiscountType DiscountType { get; private set; }

        public List<ICartItem> DiscountCondition { get; private set; }

        public List<ICartItem> DiscountCalculatedOnItems { get; private set; }

        public double DiscountFactor { get; private set; }

        public bool CanApplyRecursively
        {
            get { return this.DiscountType != DiscountType.Percentage && this.DiscountType != DiscountType.CheapestItemFree; }
        }

        public List<ICartItem> MergedCartItems
        {
            get
            {
                var merged = new List<ICartItem>();
                foreach (var item in this.DiscountCondition)
                {
                    merged.Add(new CartItem(item.Product, item.Quantity));
                }

                if (this.DiscountCalculatedOnItems != null)
                {
                    foreach (var item in this.DiscountCalculatedOnItems)
                    {
                        var matchingCartItem = merged.FirstOrDefault(o => o.Product.Id == item.Product.Id);
                        if (matchingCartItem != null)
                        {
                            matchingCartItem.Increment(item.Quantity);
                        }
                        else
                        {
                            merged.Add(new CartItem(item.Product, item.Quantity));
                        }
                    }
                }

                return merged;
            }
        }
    }
}
