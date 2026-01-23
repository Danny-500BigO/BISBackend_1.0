using BakeryApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace BakeryApi.Application.Services
{

    public class ProductService : IProductService 
    {
        private readonly List<Product>  _prodcuts = new List<Product>();

        public async Task<Product> AddProductAsync(Product product)
        {
            product.Product_Id = _prodcuts.Count +  1;
            _prodcuts.Add(product);
            return await Task.FromResult(product);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = _prodcuts.FirstOrDefault(p => p.Product_Id == id);
            return await Task.FromResult(product);
        }
    }
}