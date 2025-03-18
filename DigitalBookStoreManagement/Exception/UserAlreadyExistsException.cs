namespace DigitalBookStoreManagement.Expection
{
    public class UserAlreadyExistsException : ApplicationException
    {
        public UserAlreadyExistsException() { }
        public UserAlreadyExistsException(string msg) : base(msg) { }
    }
}
