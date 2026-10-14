using BakeryApi.Domain.Entities;
using System.Threading.Tasks;


namespace BakeryApi.Application.Services.Email
{

    public interface IEmailService
    {
               Task SendEmailConfirmationAsync(string token, string email);
        

    }

}