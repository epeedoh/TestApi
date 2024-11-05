using EPE_AuthLibrary.Interfaces;
using EPE_AuthLibrary.Models;

namespace TestApi.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        public async Task CreateUserAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;

        }

        public async Task<User> GetUserByUsernameOrEmailOrPhoneAsync(string identifier)
        {
           var user = _users.FirstOrDefault(u =>
           u.PhoneNumber == identifier ||
           u.Email == identifier ||
           u.Username == identifier);
             return await Task.FromResult(user); 
        }
    }
}
