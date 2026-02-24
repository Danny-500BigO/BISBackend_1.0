using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BakeryApi.Domain.Entities;
using BakeryApi.Infrastructure.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BakeryApi.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly BakeryDbContext _context;
        private readonly List<User> _user = new List<User>();
        private byte saltPassword;
        private IPasswordHasher<User> _passwordhasher;

        public UserService(BakeryDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordhasher = passwordHasher;
        }

        //Register new user
        public async Task<User> AddUserAsync(User user)
        {
            //send pasword to salting
            var passwordHash = CreatePasswordWithHash(user.password);

            user.password = passwordHash.PasswordHash;
            user.salt = passwordHash.salt;
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return await Task.FromResult(user);
        }

        //password hashing and salt
        public (string PasswordHash, string salt) CreatePasswordWithHash(string password)
        {
            //create salt(1)
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            //creat Hash(2)
            var hash = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8
                )
            );

            return (hash, Convert.ToBase64String(salt));
        }

        //view user byId
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.user_id == id);
            return user;
        }

        //user Login
        public async Task<LoginResponseDto?> LoginAsync(LoginRequest request)
        {

            //check the user
            var isUser = await _context.User.FirstOrDefaultAsync(u =>
                u.user_name == request.user_name
            );

            if (isUser == null)
            {
                return new LoginResponseDto { ErrorMessage = "User name or password is incorrect" };
            }

            //verfy the passwords
            var verifyPassword = _passwordhasher.VerifyHashedPassword(
                isUser,
                isUser.password,
                isUser.salt,
                request.password
            );
            if (verifyPassword == PasswordVerificationResult.Failed)
                return null; // wrong password

            return new LoginResponseDto { user_name = isUser.user_name };
        }
    }
}
