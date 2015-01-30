using ANTIL.Domain.Entities;

namespace ANTIL.Domain.Repositoies
{
    public class AntilFileRepository
    {
        public void Save(AntilFile file)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(file);
                    transaction.Commit();
                } 
            }
        }
    }
}
