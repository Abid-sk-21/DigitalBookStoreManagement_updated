using DigitalBookStoreManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalBookStoreManagement.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly BookStoreDBContext context;

        public UserRepo(BookStoreDBContext context)
        {
            this.context = context;
        }
        public List<User> GetUserInfo()
        {
            return context.Users.FromSqlRaw("exec GetAllUsers").AsEnumerable().ToList();

        }

        public User GetUserInfo(int id)
        {
            
            return context.Users.FirstOrDefault(x => x.UserID == id);
        }

        public int AddUser(User userInfo)
        {
            context.Users.Add(userInfo);
            return context.SaveChanges();
        }

        public int RemoveUser(int id)
        {
            User UI = context.Users.FirstOrDefault(e => e.UserID == id);
            context.Users.Remove(UI);
            return context.SaveChanges();
        }

        public int UpdateUser(int id, string password )
        {
            User UI = context.Users.FirstOrDefault( x=> x.UserID == id);
            //UI.Name = userInfo.Name;
            //UI.Email = userInfo.Email;
            UI.Password = password.ToString();
            //UI.ConfirmPassword = userInfo.ConfirmPassword;
            //UI.Role = userInfo.Role;
            //context.Entry(UI).Property(u => u.Password).IsModified = true;
            context.Users.Update(UI);
            return context.SaveChanges();

        }

        
    }
}
