using ShoppingCart.DiscountLib;
using ShoppingCart.DiscountLib.CartManager;
using ShoppingCart.DiscountLib.DiscountCalculator;
using ShoppingCart.DiscountLib.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace ShoppingCart.Test
{
    public class MultipleDiscountFunctionalTest
    {
        private readonly ConcurrentDictionary<string, IProduct> productInventory;
        private readonly List<IDiscountParameters> discounts;


        private ITestOutputHelper OutputHelper { get; }
        public MultipleDiscountFunctionalTest(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
            Product butter = new Product(1, "Butter", 0.80);
            Product milk = new Product(2, "Milk", 1.15);
            Product bread = new Product(3, "Bread", 1.00);

            productInventory = new ConcurrentDictionary<string, IProduct>();
            productInventory[butter.Name] = butter;
            productInventory[milk.Name] = milk;
            productInventory[bread.Name] = bread;

            IDiscountParameters buy2ButterGet50percentOffOnBread = new DiscountParameters(DiscountType.PercentageDiscountOnItems, new List<ICartItem>() { new CartItem(butter, 2) }, new List<ICartItem>() { new CartItem(bread, 1) }, 50.0);
            IDiscountParameters buy3MilkGet4thFree = new DiscountParameters(DiscountType.PercentageDiscountOnItems, new List<ICartItem>() { new CartItem(milk, 3) }, new List<ICartItem>() { new CartItem(milk, 1) }, 100.0);

            discounts = new List<IDiscountParameters>() { buy2ButterGet50percentOffOnBread, buy3MilkGet4thFree };
        }
        [Theory]
        //Given the basket has 1 bread, 1 butter and 1 milk when I total the basket then the total should be £2.95
        [InlineData(1, 1, 1, 2.95, 2.95, 0, "Given the basket has 1 bread, 1 butter and 1 milk when I total the basket then the total should be £2.95")]
        //Given the basket has 2 butter and 2 bread when I total the basket then the total should be £3.10
        [InlineData(2, 0, 2, 3.10, 3.60, 1, "Given the basket has 2 butter and 2 bread when I total the basket then the total should be £3.10")]
        //Given the basket has 4 milk when I total the basket then the total should be £3.45
        [InlineData(0, 4, 0, 3.45, 4.60, 1, "Given the basket has 4 milk when I total the basket then the total should be £3.45")]
        //Given the basket has 2 butter, 1 bread and 8 milk when I total the basket then the total should be £9.00
        [InlineData(2, 8, 1, 9.00, 11.80, 3, "Given the basket has 2 butter, 1 bread and 8 milk when I total the basket then the total should be £9.00")]
        public void RequirementDocumentSecnerios(uint butterQuantity, uint milkQuantity, uint breadQuantity, double expectedCartValueAfterDiscount, double expectedCartValueBeforeDiscount,
            int expectedSuccessfullDiscounts, string message)
        {
            var butter = productInventory["Butter"];
            var milk = productInventory["Milk"];
            var bread = productInventory["Bread"];

            List<ICartItem> basicCartItems = new List<ICartItem>
            {
                new CartItem(butter, butterQuantity),
                new CartItem(milk, milkQuantity),
                new CartItem(bread, breadQuantity)
            };
            ICart shoppingCart = new Cart(1, basicCartItems);

            ICartManager cartManager = new CartManager(shoppingCart, discounts);
            cartManager.Build();
            var cartValueAfterDiscount = cartManager.Evaluate(CartEvaluationAlgorithm.HightestValueDiscountFirst, out List<IDiscountParameters> successfullDiscounts, out double cartValueBeforeDiscount);
            cartValueAfterDiscount = System.Math.Round(cartValueAfterDiscount, 2);
            cartValueBeforeDiscount = System.Math.Round(cartValueBeforeDiscount, 2);
            OutputHelper.WriteLine(message + " and it is £" + cartValueAfterDiscount);
            Assert.Equal(expectedCartValueAfterDiscount, cartValueAfterDiscount);
            Assert.Equal(expectedCartValueBeforeDiscount, cartValueBeforeDiscount);
            Assert.Equal(expectedSuccessfullDiscounts, successfullDiscounts.Count);
        }
    }
}
