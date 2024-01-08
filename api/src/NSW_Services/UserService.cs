using NSW.Data;
using NSW.Data.DTO.Request;
using NSW.Data.DTO.Response;
using NSW.Data.Interfaces;
using NSW.Repositories.Interfaces;
using NSW.Services.Interfaces;

namespace NSW.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IRepository<PostalCode> _postalCodeRepository;

        public UserService(
            IUserRepository repository,
            IRepository<PostalCode> postalCodeRepository
            )
        {
            _postalCodeRepository = postalCodeRepository;
            _repository = repository;
        }

        public void Delete(UserRequest entity)
        {
            var user = GetUserFromUserRequest(entity);
            _repository.Delete(user);
        }

        public bool ExistsByEmail(string email) => _repository.ExistsByEmail(email);
        public bool ExistsById(int id) => _repository.ExistsById(id);

        public IList<UserResponse> GetAll()
        {
            var returnValue = new List<UserResponse>();
            var users = _repository.GetAll();
            foreach (var user in users)
            {
                returnValue.Add(GetUserResponseFromUser(user));
            }
            return returnValue;
        }

        public UserResponse GetByEmail(string email)
        {
            var user = _repository.GetByEmail(email);
            return GetUserResponseFromUser(user);
        }

        public UserResponse? GetById(int id)
        {
            var user = _repository.GetById(id);
            return GetUserResponseFromUser(user);
        }


        public UserResponse Insert(UserRequest entity)
        {

            var user = GetUserFromUserRequest(entity);
            var result = _repository.Insert(user);
            return GetUserResponseFromUser(result);

        }

        public UserResponse Modify(UserRequest entity)
        {
            var user = GetUserFromUserRequest(entity);
            var result = _repository.Modify(user);
            return GetUserResponseFromUser(result);
        }

        private User GetUserFromUserRequest(UserRequest request)
        {
            var user = new User
            {
                LanguagePreference = request.LanguagePreference,
                Email = request.Email,
                Phone = request.Phone,
                PostalCode = request.PostalCode,
                UserName = request.UserName,
                Id = request.Id,
            };
            return user;
        }

        private UserResponse GetUserResponseFromUser(User input)
        {
            var postalCode = _postalCodeRepository.GetByIdentifier(input.PostalCode);
            var returnValue = new UserResponse
            {
                Id = input.Id,
                Email = input.Email,
                Phone = input.Phone,
                LanguagePreference = input.LanguagePreference,
                Role = input.Role,
                UserName = input.UserName,
                PostalCode = new PostalCodeResponse
                {
                    Code = postalCode.Code,
                    Longitude = postalCode.Longitude,
                    Latitude = postalCode.Latitude
                }
            };
            return returnValue;
        }

    }
}
