using DigitalBookStoreManagement.Model;

namespace DigitalBookStoreManagement.Service
{
    public interface IUserService
    {
        List<User> GetUserInfo();
        User GetUserInfo(int id);

        User GetUserInfo(string email);

        int AddUser(User userInfo);

        int RemoveUser(int id);

        int UpdateUser(string email, string password);

        
    }
}
