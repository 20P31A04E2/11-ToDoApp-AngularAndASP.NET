using Repository.DataConcerns;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoContext _todoContext;

        public UserRepository(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public bool AddUser(User newUser)
        {
            var existingUser = _todoContext.Users.FirstOrDefault(u => u.UserName == newUser.UserName && u.Password == newUser.Password);

            if (existingUser != null)
            {
                return true;
            }
            _todoContext.Users.Add(newUser);
            _todoContext.SaveChanges();
            return false;
        }

        public User VerifyUser(User currentUser)
        {
            var user = _todoContext.Users.SingleOrDefault(u => u.UserName == currentUser.UserName && u.Password == currentUser.Password);
            return user;
        }
    }
}
