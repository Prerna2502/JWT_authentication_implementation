using AuthenticationService.Exceptions;
using AuthenticationService.Models;
using AuthenticationService.Repository;

namespace AuthenticationService.Service
{
    public class AuthService : IAuthService
    {
        readonly IAuthRepository _repository;
        public AuthService(IAuthRepository repository)
        {
            _repository = repository;
        }
        public bool LoginUser(User user)
        {
            var result = _repository.LoginUser(user);
            if (result)
                return result;
            throw new UserNotFoundException("Invalid user id or password");
        }

        public bool RegisterUser(User user)
        {
            var result = false;
            if (_repository.IsUserExists(user.UserId) == false)
            {
                result = _repository.CreateUser(user);
            }
            if (result)
                return result;
            throw new UserAlreadyExistsException($"This userId {user.UserId} already in use");
        }
    }
}
