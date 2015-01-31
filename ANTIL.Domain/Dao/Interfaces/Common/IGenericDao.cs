using System.Collections.Generic;
using System.Linq;
using ANTIL.Domain.Entities.Common;

namespace ANTIL.Domain.Dao.Interfaces.Common
{
    public interface IGenericDao<T> where T : DomainObject
    {
        IList<T> Get();
        
        IList<T> Get(ICollection<int> ids);
        
        T Get(int id);
        
        int Count();
        
        void Delete(T entity);
        
        void BulkDelete(ICollection<T> entities);
        
        void Delete(int id);
        
        T Save(T entity);
        
        void BulkSave(ICollection<T> entities);
        
        IQueryable<T> CreateQuery();
    }
}