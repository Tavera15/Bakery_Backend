using API.Core.CustomExceptions;
using API.Core.DTOs.Products;
using API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAccess.SQL.Services
{
    public class ProductService : BaseRepository<Product>
    {
        public ProductService(DataContext c) : base(c) { }

        public override async Task<Product> UpdateAsync(string entityId, Product updatedEntity)
        {
            Product target = await FindAsync(entityId);

            if(target == null)
            {
                throw new EntityNotFoundException("Product not found: " + entityId);
            }

            target.name = updatedEntity.name;
            target.mDescription = updatedEntity.mDescription;
            target.mUnitPrice = updatedEntity.mUnitPrice;
            target.mIsAvailable = updatedEntity.mIsAvailable;
            target.mTimeModified = updatedEntity.mTimeModified;

            await _context.SaveChangesAsync();
            return target;
        }
    }
}
