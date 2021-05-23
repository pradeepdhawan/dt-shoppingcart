using ShoppingCart.DiscountLib;
using ShoppingCart.DiscountLib.DiscountCalculator;
using ShoppingCart.DiscountLib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using ShoppingCart.DiscountLib.CustomException;

namespace ShoppingCart.Test
{
    public class CartUnitTest
    {
        private ITestOutputHelper OutputHelper { get; }
        public CartUnitTest(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }

        [Fact]
        public void TestRemoveInvalidCartItem()
        {
            ICart cart = new Cart(1, new List<ICartItem>() { new CartItem(new Product(1, "Butter", 0.80), 5) });
            
            var ex = Assert.Throws<InvalidInputException>(() => cart.Remove(new CartItem(new Product(2, "Milk", 1.15), 5)));

            Assert.Equal("Product is not in cart :Milk", ex.Message);
        }

        [Fact]
        public void TestRemoveMoreThanInCartItem()
        {
            ICart cart = new Cart(1, new List<ICartItem>() { new CartItem(new Product(1, "Butter", 0.80), 5) });

            var ex = Assert.Throws<InvalidInputException>(() => cart.Remove(new CartItem(new Product(1, "Butter", 1.15), 15)));

            Assert.Equal("Product is quantity in cart is : 5 and required to remove is : 15", ex.Message);
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
