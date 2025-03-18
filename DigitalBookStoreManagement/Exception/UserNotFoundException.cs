namespace DigitalBookStoreManagement.Expection
{
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException() { }
        public UserNotFoundException(string msg) : base(msg) { }
    }
}
