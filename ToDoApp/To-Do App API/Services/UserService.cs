using AutoMapper;
using Concerns;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public bool AddUser(User newUser)
        {
            return _userRepository.AddUser(_mapper.Map<Repository.DataConcerns.User>(newUser));
        }

        public User VerifyUser(User currentUser)
        {
            var user = _userRepository.VerifyUser(_mapper.Map<Repository.DataConcerns.User>(currentUser));
            if(user != null)
            {
                return _mapper.Map<User>(user);
            }
            else
            {
                return null;
            }

        }

        private static Repository.DataConcerns.User ConvertToRepositoryUser(User concernsUser)
        {
            Repository.DataConcerns.User RepositoryUser = new Repository.DataConcerns.User();
            RepositoryUser.UserName = concernsUser.UserName;
            RepositoryUser.Password = concernsUser.Password;
            return RepositoryUser;
        }

        private User ConvertToConcernsUser(Repository.DataConcerns.User repositoryUser)
        {
            var concernsUser = new User
            {
                UserId = repositoryUser.UserId,
                UserName = repositoryUser.UserName,
                Password = repositoryUser.Password,
            };
            return concernsUser;
        }
    }
}
