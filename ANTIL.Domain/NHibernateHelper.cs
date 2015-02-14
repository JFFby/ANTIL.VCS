using System;
using ANTIL.Domain.Core.Entities.Common;
using ANTIL.Domain.Dao.Implementations.Common;
using Castle.MicroKernel.Registration;
using NHibernate;
using NHibernate.Cfg;

namespace ANTIL.Domain
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    InitSessionFactory();
                }

                return _sessionFactory;
            }
        }

        private static void InitSessionFactory()
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            _sessionFactory = configuration.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
