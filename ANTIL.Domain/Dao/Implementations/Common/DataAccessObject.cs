using System.Collections.Generic;
using System.Linq;
using ANTIL.Domain.Core.Entities.Common;
using ANTIL.Domain.Dao.Interfaces.Common;
using NHibernate;
using NHibernate.Linq;

namespace ANTIL.Domain.Dao.Implementations.Common
{
    public class DataAccessObject : IDataAccessObject
    {
        private ISession NHibernateSession
        {
            get
            {
                return NHibernateHelper.OpenSession();
            }
        }

        public T Load<T>(int id) where T : DomainObject
        {
            return NHibernateSession.Load<T>(id);
        }

        public IQueryable<T> Query<T>() where T : DomainObject
        {
            return NHibernateSession.Query<T>();
        }

        public T Save<T>(T entity) where T : DomainObject
        {
            if (entity == null)
                return null;

            using (var transaction = BeginTransaction())
            {
                NHibernateSession.SaveOrUpdate(entity);
                transaction.Commit();
            }
            return entity;
        }

        public void BulkSave<T>(ICollection<T> entities) where T : DomainObject
        {
            if (entities == null)
                return;

            using (var transaction = BeginTransaction())
            {
                foreach (var entity in entities)
                {
                    NHibernateSession.SaveOrUpdate(entity);
                }

                transaction.Commit();
            }
        }

        public void Delete<T>(T entity) where T : DomainObject
        {
            if(entity == null)
                return;

            using (var transaction = BeginTransaction())
            {
                NHibernateSession.Delete(entity);
                transaction.Commit();
            }
        }

        public void BulkDelete<T>(ICollection<T> entities) where T : DomainObject
        {
            if (entities == null)
                return;

            using (var transaction = BeginTransaction())
            {
                foreach (var entity in entities)
                {
                    NHibernateSession.Delete(entity);
                }

                transaction.Commit();
            }
        }

        public ISession GetSession()
        {
            return NHibernateSession;
        }

        public IDataBaseTransaction BeginTransaction()
        {
            return new DataBaseTransaction(NHibernateSession.BeginTransaction());
        }
    }
}
