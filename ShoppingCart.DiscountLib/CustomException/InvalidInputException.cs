namespace ShoppingCart.DiscountLib.CustomException
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidInputException : Exception
    {
        private readonly string property;

        public InvalidInputException(string property = "")
        {
            this.property = property;
        }

        public InvalidInputException(string message, string property = "")
            : base(message)
        {
            this.property = property;
        }

        public InvalidInputException(string message, System.Exception innerException, string property = "")
            : base(message, innerException)
        {
            this.property = property;
        }

        protected InvalidInputException(SerializationInfo info, StreamingContext context, string property)
            : base(info, context)
        {
            this.property = property;
        }
    }
}