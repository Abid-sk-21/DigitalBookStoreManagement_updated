namespace DigitalBookStoreManagement.Authentication
{
    public interface IAuth
    {
        string Authentication(string email, string password);
    }
}
