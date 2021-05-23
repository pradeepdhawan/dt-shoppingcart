namespace ShoppingCart.DiscountLib.Model
{
    using ShoppingCart.DiscountLib.DiscountCalculator;

    public class DiscountByWeightage
    {
        public double Weightage { get; private set; }

        public IDiscountParameters Discount { get; private set; }

        public DiscountByWeightage(double weightage, IDiscountParameters discount)
        {
            this.Weightage = weightage;
            this.Discount = discount;
        }
    }
}
