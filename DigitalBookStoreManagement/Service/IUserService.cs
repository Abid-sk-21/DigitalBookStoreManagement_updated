using DigitalBookStoreManagement.Model;

namespace DigitalBookStoreManagement.Service
{
    public interface IUserService
    {
        List<User> GetUserInfo();
        User GetUserInfo(int id);

        int AddUser(User userInfo);

        int RemoveUser(int id);

        int UpdateUser(int id, string password);

        
    }
}
