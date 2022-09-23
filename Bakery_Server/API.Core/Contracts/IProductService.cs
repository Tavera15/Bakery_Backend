using API.Core.DTOs.Products;
using API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Contracts
{
    public interface IProductService : IRepository<Product>
    {
        Task<Product> AddImagesToProduct(string productId, IEnumerable<ProductImageMakerDTO> images);
        Task<Product> DeleteProductImage(string productId, string imageId);
    }
}
