using OAuth.Business.Contract.Interface;
using OAuth.Business.Contract.Models;
using OAuth.Business.Service.AuthenticationManager;
using OAuth.Data.Contract.Interface;
using OAuth.Data.Contract.Models;
using System;
using System.Threading.Tasks;

namespace OAuth.Business.Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserDataService _userDataService;
        private readonly IUserCredentialDataService _userCredentialDataService;
        public UserService(IUserDataService userDataService, IUserCredentialDataService userCredentialDataService)
        {
            _userDataService = userDataService;
            _userCredentialDataService = userCredentialDataService;
        }
        public async Task<string> AddNewUser(UserViewModel userModel)
        {
            try
            {
                var userId = await _userDataService.CreateUser(CreateUserDataModel(userModel));
                if (userId > 0)
                {
                    var credId = await _userCredentialDataService.CreateUserCredential(CreateUserCredentialDataModel(userModel, userId));
                    return credId > 0 ? userModel.UserName : "failed";
                }
            }
            catch (Exception)
            {
                throw new Exception($"Failed to create user {userModel.UserName}");
            }
            return "failed";
        }
        public bool ValidUser(UserLoginModel login)
        {
            return _userCredentialDataService.AuthenticateUserCredential(login.UserName, CrptoPasswordHash.EncodePasswordToBase64(login.Password));
        }

        private User CreateUserDataModel(UserViewModel userModel)
        {
            var user = new User
            {
                FirstName = userModel.FirstName,
                MiddleName = userModel.MiddleName,
                LastName = userModel.LastName,
                RegisteredAt = DateTime.Now,
                LastLogin = DateTime.Now,
                Intro = userModel.Intro,
                Profile = userModel.Profile,
                IsActive = true
            };
            return user;
        }
        private UserCredential CreateUserCredentialDataModel(UserViewModel userModel, long userId)
        {
            var userCredential = new UserCredential
            {
                UserName = userModel.UserName,
                Password = CrptoPasswordHash.EncodePasswordToBase64(userModel.Password),
                RegisterAt = DateTime.Now,
                IsActive = true,
                UserId = userId
            };
            return userCredential;
        }
    }
}
