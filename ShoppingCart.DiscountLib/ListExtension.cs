namespace ShoppingCart.DiscountLib
{
    using System.Collections.Generic;

    public static class ListExtension
    {
        public static List<T> GetClone<T>(this List<T> source)
        {
            return source.GetRange(0, source.Count);
        }
    }
}
