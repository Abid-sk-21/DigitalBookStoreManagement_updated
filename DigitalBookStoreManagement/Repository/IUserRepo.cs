using DigitalBookStoreManagement.Model;

namespace DigitalBookStoreManagement.Repository
{
    public interface IUserRepo
    {
        List<User> GetUserInfo();
        User GetUserInfo(int id);

        User GetUserInfo(string email);

        int AddUser(User userInfo);

        int RemoveUser(int id);

        int UpdateUser(string email, string password);

        
    }
}
