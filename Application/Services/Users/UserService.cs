using System.Collections.Generic;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BakeryApi.Application.Services.Email;
using BakeryApi.Domain.Entities;
using BakeryApi.Infrastructure.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BakeryApi.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly BakeryDbContext _context;
        private readonly List<User> _user = new List<User>();
        private byte saltPassword;
        private IPasswordHasher<User> _passwordhasher;
        private readonly JwtService _jwtService;

        private readonly IEmailService _emailservice;
        private readonly ILogger<UserService> _logger;

        public UserService(BakeryDbContext context, IPasswordHasher<User> passwordHasher, JwtService jwtService, ILogger<UserService> logger, IEmailService emailService)
        {
            _context = context;
            _passwordhasher = passwordHasher;
            _jwtService = jwtService;
            _logger = logger;
            _emailservice = emailService;
        }

        //Register new user
        public async Task<UserResponseDto> AddUserAsync(UserRequestDto user)
        {
            var newUser = new User
            {
                first_name = user.first_name,
                last_name = user.last_name,
                email = user.email,
                date_of_birth = user.date_of_birth,
                user_name = user.user_name,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                is_active = true
            };
            //send pasword to salting
            var passwordHash = CreatePasswordWithHash(newUser, user.password);

            newUser.password = passwordHash;
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            var newResponse = new UserResponseDto
            {
                first_name = user.first_name,
                last_name = user.last_name,
                email = user.email,
                date_of_birth = user.date_of_birth,
                user_name = user.user_name,
                password = passwordHash,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow,
                is_active = true
            };
            return newResponse;
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
            return new LoginResponseDto
            {
                user_name = isUser.user_name,
                SuccessMessage = "Login Succefull",

                Token = token

            };
        }

        public async Task<ResetPasswordResponseDto> SendResetEmailAsync(ResetPasswordRequestDto request)
        {

            try
            {
                //check the requesting user exixsting  in  the database
                var isUserExist = await _context.User.FirstOrDefaultAsync(u => u.email == request.email);

                //if results null it goeserror message user not found
                if (isUserExist != null)
                {

                    //get the ratelimit values from datatabse
                    var checkResetHistory = await _context.ResetPassword.FirstOrDefaultAsync(u => u.user_id == isUserExist.user_id);

                    if (checkResetHistory != null)
                    {

                        var elapsed = DateTime.UtcNow - checkResetHistory?.last_reset_timeDate;


                        if (checkResetHistory?.rate_limit > 3)
                        {
                            Console.WriteLine("your password reset limit exceed, please try again");
                            return new ResetPasswordResponseDto
                            {
                                ErrorMessage = "your password reset limit exceed, please try again"
                            };
                        }
                        else if (elapsed.HasValue && elapsed?.TotalMinutes > 240)
                        {
                            Console.WriteLine("please try in another hour, you can reset only one time  in each 4hour");
                            return new ResetPasswordResponseDto
                            {
                                ErrorMessage = "please try in another hour, you can reset only one time  in each 4hour"
                            };
                        }
                        else
                        {

                            byte[] tokenBytes = RandomNumberGenerator.GetBytes(32);

                            string token = Convert.ToBase64String(tokenBytes)
                                .TrimEnd('=')
                                .Replace('+', '-')
                                .Replace('/', '_');


                            await _emailservice.SendEmailConfirmationAsync(
                             token,
                             request.email

                         );

                            return new ResetPasswordResponseDto
                            {
                                ErrorMessage = "please try in another hour, you can reset only one time  in each 4hour"
                            };
                        }


                    }
                    else
                    {

                        //start from here

                        byte[] tokenBytes = RandomNumberGenerator.GetBytes(32);

                        string token = Convert.ToBase64String(tokenBytes)
                            .TrimEnd('=')
                            .Replace('+', '-')
                            .Replace('/', '_');

                        await _emailservice.SendEmailConfirmationAsync(
                         token,
                         request.email
                     );
                        return new ResetPasswordResponseDto
                        {
                            ErrorMessage = "please try in another hour, you can reset only one time  in each 4hour"
                        };

                    }


                }
                else
                {
                    return new ResetPasswordResponseDto
                    {
                        ErrorMessage = "User Not found",
                        email = request.email
                    };
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred during password reset");

                throw;
            }



        }
    }
}

