using DigitalBookStoreManagement.Expections;
using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Repository;

namespace DigitalBookStoreManagement.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo repo;

        public UserService(IUserRepo repo)
        {
            this.repo = repo;
        }
        public List<User> GetUserInfo()
        {
            return repo.GetUserInfo();
        }
       //Get user by Id
        public User GetUserInfo(int id)
        {
            User info = repo.GetUserInfo(id);
            if (info == null)
            {
                throw new UserNotFoundException($"No User found with id {id}");
            }
            return info;
        }

        //Get user by email
        public User GetUserInfo(string email)
        {
            User info = repo.GetUserInfo(email);
            if (info == null)
            {
                throw new UserNotFoundException($"No User found with email {email}");
            }
            return info;
        }



        //Insert user 
        public int AddUser(User userInfo)
        {
            if (repo.GetUserInfo(userInfo.UserID) != null)
            {
                throw new UserAlreadyExistsException($"The user already exists");
            }
            return repo.AddUser(userInfo);
         
        }

        //Delete User
        public int RemoveUser(int id)
        {
            if (repo.GetUserInfo(id) == null)
            {
                throw new UserNotFoundException($"No user exists with id {id}");
            }
            return repo.RemoveUser(id);
        }

        //Update user 
        public int UpdateUser(string email, string password)

        {

            if (repo.GetUserInfo(email) == null)
            {
                throw new UserNotFoundException($"User do not exists with this email {email}");
            }
            return repo.UpdateUser(email, password);
        }

        
       

    }
}
