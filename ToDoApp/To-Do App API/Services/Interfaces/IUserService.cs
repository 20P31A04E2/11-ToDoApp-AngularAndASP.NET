using Concerns;

namespace Services.Interfaces
{
    public interface IUserService
    {
        public bool AddUser(User newUser);
        public User VerifyUser(User currentUser);
    }
}
