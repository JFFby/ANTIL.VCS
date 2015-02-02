using System.Collections.Generic;
using System.Linq;
using ANTIL.Domain.Core.Entities.Common;
using ANTIL.Domain.Dao.Interfaces.Common;

namespace ANTIL.Domain.Dao.Implementations.Common
{
    public class GenericDao<T> : IGenericDao<T> where T : DomainObject
    {
        private readonly IDataAccessObject _dao;

        public GenericDao(IDataAccessObject dao)
        {
            this._dao = dao;
        }

        protected IDataAccessObject Dao
        {
            get { return _dao; }
        }

        public IList<T> Get()
        {
            return Dao.Query<T>().ToList();
        }

        public IList<T> Get(ICollection<int> ids)
        {
            return Dao.Query<T>().Where(x => ids.Contains(x.Id)).ToList();
        }

        public T Get(int id)
        {
            return Dao.Load<T>(id);
        }

        public int Count()
        {
            return Dao.Query<T>().Count();
        }

        public virtual void Delete(T entity)
        {
            Dao.Delete(entity);
        }

        public virtual void BulkDelete(ICollection<T> entities)
        {
            Dao.BulkDelete(entities);
        }

        public virtual void Delete(int id)
        {
            var entity = Get(id);
            Dao.Delete(entity);
        }

        public T Save(T entity)
        {
            return Dao.Save(entity);
        }

        public void BulkSave(ICollection<T> entities)
        {
            Dao.BulkSave(entities);
        }

        public IQueryable<T> CreateQuery()
        {
            return Dao.Query<T>();
        }
    }
}
