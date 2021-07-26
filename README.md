# dt-shoppingcart


Requirement: We have a Shopping Cart project that calculates the price of the items in the Cart after applying all available offers.

There are four types of discounts (offers) that we can apply to cart:
- Absolute Discount (on all items) based on condition
- Percentage Discount (on all items) based on condition
- Absolute Discount (on some items) based on condition
- Percentage Discount (on some items) based on condition

There is further scope to support more type of discounts (offers):
- Absolute discount with minimum spend criteria
- Cheapest item free 
and list goes on

Product class in combination with CartItem allows you to define Inventory of Products. 
Example 5 pieces of Butter sold at price £ 0.8 each are added to cart.

Cart Evaluation class allows you to evaluate various discounts that you can apply on the cart.
There are various ways to choose which discount (offer) should be given preference like MaximiseProfit, HighestDiscountFirst, SmallestSizeBatchFirst etc.
For simplicity we have implemented HighestDiscountFirst algorithm, though safest should have been MaximiseProfit algorithm but we didn't do that as it is fairly big project to implement that. 
There are dedicated optimization api's and products available in the market (like NAG library) dedicated to that and it be unjustified to attempt that for an exercise.

Based on assignment Functional test cases have been implemented in DT-ShoppingCart\ShoppingCart.Test\MultipleDiscountFunctionalTest.cs 

Offers (refer to contructor)
- Buy 2 Butter and get a Bread at 50% off
- Buy 3 Milk and get the 4th milk for free
Scenarios(Refer to Theory and each inline represent the 4 secnerios)
- Given the basket has 1 bread, 1 butter and 1 milk when I total the basket then the total should be £2.95
- Given the basket has 2 butter and 2 bread when I total the basket then the total should be £3.10
- Given the basket has 4 milk when I total the basket then the total should be £3.45
- Given the basket has 2 butter, 1 bread and 8 milk when I total the basket then the total should be £9.00

there are a lot more unit test cases and integration test cases besides the functional test covring core requirements.

![picture](https://github.com/pradeepdhawan/dt-shoppingcart/blob/main/Class%20Diagram.png)
