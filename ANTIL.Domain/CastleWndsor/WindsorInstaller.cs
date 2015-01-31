﻿using ANTIL.Domain.Dao.Implementations;
using ANTIL.Domain.Dao.Implementations.Common;
using ANTIL.Domain.Dao.Interfaces;
using ANTIL.Domain.Dao.Interfaces.Common;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace ANTIL.Domain.CastleWndsor
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().IncludeNonPublicTypes()
                .BasedOn(typeof(IGenericDao<>)).WithServiceDefaultInterfaces().LifestyleTransient());

            //container.Register(Component.For(typeof(IAntilFileDao)).ImplementedBy(typeof(AntilFileDao)));
            container.Register(Component.For<IDataBaseTransaction>().ImplementedBy<DataBaseTransaction>());
            container.Register(Component.For<IDataAccessObject>().ImplementedBy<DataAccessObject>());
            //container.Register(Component.For(typeof(IGenericDao<>)).ImplementedBy(typeof(GenericDao<>)));
        }
    }
}
