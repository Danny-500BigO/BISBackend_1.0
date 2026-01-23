using BakeryApi.Domain.Entities;
using System.Threading.Tasks;

namespace BakeryApi.Application.Services
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
    }
}
