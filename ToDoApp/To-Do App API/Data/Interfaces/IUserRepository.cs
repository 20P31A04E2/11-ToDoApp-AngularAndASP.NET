using Repository.DataConcerns;

namespace Repository.Interfaces
{
    public interface  IUserRepository
    {
        public bool AddUser(User newUser);

        public User VerifyUser(User currentUser);
    }
}
