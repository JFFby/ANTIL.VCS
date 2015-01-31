using System;

namespace ANTIL.Domain.Dao.Interfaces.Common
{
    public interface IDataBaseTransaction : IDisposable
    {
        void Commit();

        void RollBack();
    }
}