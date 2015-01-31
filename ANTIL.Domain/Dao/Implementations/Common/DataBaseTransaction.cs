using ANTIL.Domain.Dao.Interfaces.Common;
using NHibernate;

namespace ANTIL.Domain.Dao.Implementations.Common
{
    public class DataBaseTransaction : IDataBaseTransaction
    {
        private readonly ITransaction _transaction;

        public DataBaseTransaction(ITransaction transaction)
        {
            this._transaction = transaction;
        }

        public void Commit()
        {
            if (!_transaction.WasCommitted)
            {
                _transaction.Commit();
            }
        }

        public void RollBack()
        {
            if (!_transaction.WasRolledBack)
            {
                _transaction.Rollback();
            }
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
