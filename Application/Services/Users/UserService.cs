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
        private readonly JwtService _jwtService;

        public UserService(BakeryDbContext context, IPasswordHasher<User> passwordHasher ,JwtService jwtService)
        {
            _context = context;
            _passwordhasher = passwordHasher;
            _jwtService = jwtService;
        }

        //Register new user
        public async Task<User> AddUserAsync(User user)
        {
            //send pasword to salting
            var passwordHash = CreatePasswordWithHash(user, user.password);

             user.password = passwordHash;
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return await Task.FromResult(user);
        }

        //password hashing and salt
        public string CreatePasswordWithHash(User user, string password)
        {
            
            //password create with salt
            var newPasswordandSalt = _passwordhasher.HashPassword(
                user,
                password
            );

            Console.WriteLine(newPasswordandSalt);

            return newPasswordandSalt;
            
    
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
                request.password
            );
            if (verifyPassword == PasswordVerificationResult.Failed)
                return null; // wrong password

            var token = _jwtService.GenerateToken(isUser);
            return new LoginResponseDto { user_name = isUser.user_name,  SuccessMessage = "Login Succefull" ,
            
            Token = token

             };
        }
    }
}
