namespace DigitalBookStoreManagement.Expections
{
    public class OrderManagamentExceptions : Exception
    {
        public OrderManagamentExceptions(string message) : base(message) { }

        public OrderManagamentExceptions(string message, Exception innerException) : base(message, innerException) { }


    }

    public class OutOfStockException : OrderManagamentExceptions
    {

        public OutOfStockException(int BookID) : base($"Book Is Out Of Stock.") { }


    }
    public class QuantityNotAvailable : OrderManagamentExceptions
    {

        public QuantityNotAvailable(int BookID) : base($"The Ordered Quantity is greater than the Stock Available.") { }


    }
    public class OrderNotFoundException : OrderManagamentExceptions
    {
        public OrderNotFoundException(int OrderID) : base($"Order with ID {OrderID} was not found.") { }



    }

    public class InvalidOrderStatusExceptions : OrderManagamentExceptions
    {

        public InvalidOrderStatusExceptions(string status) : base($"Order Status {status} is not found.") { }


    }

 



}


