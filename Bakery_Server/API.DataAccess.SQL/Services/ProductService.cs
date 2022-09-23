using API.Core.Contracts;
using API.Core.CustomExceptions;
using API.Core.DTOs.Products;
using API.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAccess.SQL.Services
{
    public class ProductService : BaseRepository<Product>, IProductService
    {
        private readonly DbSet<ProductImage> _productImagesDbSet;
        private readonly DbSet<BasketItem> _basketItemsDbSet;

        public ProductService(DataContext c) : base(c) 
        {
            _productImagesDbSet = c.Set<ProductImage>();
        }

        public override IEnumerable<Product> GetAllEntities()
        {
            var allProducts = _dbSet.Include(p => p.productImages);

            return allProducts.OrderByDescending(x => x.mTimeModified);
        }

        public override async Task<Product> FindAsync(string entityId)
        {
            IEnumerable<Product> allProducts = GetAllEntities();
            Product target = allProducts.FirstOrDefault(x => x.mID == entityId);

            if (target == null)
            {
                throw new EntityNotFoundException("Product not found");
            }

            return await Task.FromResult(target);
        }

        public override async Task<Product> UpdateAsync(string entityId, Product updatedEntity)
        {
            Product target = await FindAsync(entityId);

            if(target == null)
            {
                throw new EntityNotFoundException("Product not found: " + entityId);
            }

            target.name                 = updatedEntity.name;
            target.mDescription         = updatedEntity.mDescription;
            target.mUnitPrice           = updatedEntity.mUnitPrice;
            target.mIsAvailable         = updatedEntity.mIsAvailable;
            target.mAvailableSizes      = updatedEntity.mAvailableSizes;
            target.mTimeModified        = updatedEntity.mTimeModified;

            await _context.SaveChangesAsync();
            return target;
        }

        public override async Task<int> DeleteAsync(string entityID)
        {
            Product target = await FindAsync(entityID);
            await ClearProductImages(target);
            await RemoveProductFromBaskets(target);

            return await base.DeleteAsync(entityID);
        }

        public async Task<Product> AddImagesToProduct(string productId, IEnumerable<ProductImageMakerDTO> images)
        {
            Product product = await FindAsync(productId);

            if (product == null)
            {
                throw new EntityNotFoundException("Product does not exists.");
            }

            foreach (ProductImageMakerDTO img in images)
            {
                ProductImage productImage = new ProductImage(img, product);
                _productImagesDbSet.Add(productImage);
            }

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProductImage(string productId, string imageId)
        {
            Product product = await FindAsync(productId);
            ProductImage target = product.productImages.SingleOrDefault(x => x.mID == imageId);

            if (target == null)
            {
                throw new EntityNotFoundException("Product Image not found");
            }

            _productImagesDbSet.Remove(target);
            await _context.SaveChangesAsync();

            return product;
        }

        private async Task<Product> ClearProductImages(Product product)
        {
            foreach (ProductImage image in product.productImages)
            {
                _productImagesDbSet.Remove(image);
            }

            await _context.SaveChangesAsync();
            return product;
        }

        private async Task<Product> RemoveProductFromBaskets(Product product)
        {
            IEnumerable<BasketItem> allBasketItems = _basketItemsDbSet.Include(x => x.product);

            foreach (BasketItem basketItem in allBasketItems.Where(b => b.product.mID == product.mID))
            {
                _basketItemsDbSet.Remove(basketItem);
            }

            await _context.SaveChangesAsync();
            return product;
        }
    }
}
