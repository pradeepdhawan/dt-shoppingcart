using ShoppingCart.DiscountLib;
using ShoppingCart.DiscountLib.DiscountCalculator;
using ShoppingCart.DiscountLib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace ShoppingCart.Test
{
    public class DiscountParametersUnitTest
    {
        private ITestOutputHelper OutputHelper { get; }
        public DiscountParametersUnitTest(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }

        [Fact]
        public void TestNegativeDiscount()
        {
            IProduct butter = new Product(1, "Butter", 0.80);
            IProduct milk = new Product(2, "Milk", 1.15);
            IProduct bread = new Product(3, "Bread", 1.00);
            

            var ex = Assert.Throws<ArgumentException>(() => new DiscountParameters(DiscountType.Absolute, new List<ICartItem>() { new CartItem(butter, 3) }, null, -1.0));

            Assert.Equal("Discount cannot be negative (Parameter 'Discount')", ex.Message);
        }

        [Fact]
        public void TestEmptyDiscountCondition()
        {
            IProduct butter = new Product(1, "Butter", 0.80);
            IProduct milk = new Product(2, "Milk", 1.15);
            IProduct bread = new Product(3, "Bread", 1.00);

            var ex = Assert.Throws<ArgumentException>(() => new DiscountParameters(DiscountType.Absolute, null, null, 1.0));

            Assert.Equal("Discount condition cannot be empty (Parameter 'Discount')", ex.Message);
        }
    }
}
