using BakeryApi.Domain.Entities;
using System.Threading.Tasks;


namespace BakeryApi.Application.Services.Users
{

    public interface IUserService
    {
        Task<UserResponseDto?> AddUserAsync(UserRequestDto user);
        Task<User> GetUserByIdAsync(int id);
        Task<LoginResponseDto?> LoginAsync(LoginRequest request);
        Task<ResetPasswordResponseDto?>ResetPasswordAsync(ResetPasswordRequestDto request);

    }

}