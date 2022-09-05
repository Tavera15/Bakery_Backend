using API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAllEntities();
        Task<T> FindAsync(string id);
        Task<T> InsertAsync(T newEntity);
        Task<int> DeleteAsync(string entityID);
        Task<T> UpdateAsync(string entityId, T updatedEntity);
    }
}
