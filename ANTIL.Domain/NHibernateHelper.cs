using System;
using System.Security.Cryptography.X509Certificates;
using ANTIL.Domain.Core.Entities.Common;
using ANTIL.Domain.Dao.Implementations.Common;
using ANTIL.Domain.Dao.Interfaces.Common;
using Castle.MicroKernel.Registration;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq.Functions;

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
            configuration.AddAssembly(typeof(IDataAccessObject).Assembly);

            var fluentModel = new PersistenceModel();
            fluentModel.AddMappingsFromAssembly(typeof(IDataAccessObject).Assembly);
            fluentModel.Configure(configuration);

            _sessionFactory = configuration.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
