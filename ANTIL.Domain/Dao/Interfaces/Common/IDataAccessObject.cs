using System.Collections.Generic;
using System.Linq;
using ANTIL.Domain.Core.Entities.Common;
using NHibernate;

namespace ANTIL.Domain.Dao.Interfaces.Common
{
    public interface IDataAccessObject
    {
        T Load<T>(int id) where T : DomainObject;

        IQueryable<T> Query<T>() where T : DomainObject;

        T Save<T>(T entity) where T : DomainObject;
        
        void BulkSave<T>(ICollection<T> entities) where T : DomainObject;
        
        void Delete<T>(T entity) where T : DomainObject;
        
        void BulkDelete<T>(ICollection<T> entities) where T : DomainObject;
        
        ISession GetSession();
    }
}