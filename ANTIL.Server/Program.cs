using System;
using System.Collections.Generic;
using System.Linq;
using ANTIL.Domain.CastleWndsor;
using ANTIL.Domain.Dao.Interfaces;
using ANTIL.Domain.Entities;
using Castle.Windsor;

namespace ANTIL.Server
{
    class Program
    {
        private static WindsorContainer IOC;

        static void Main(string[] args)
        {
            var fileList = new List<AntilFile>
            {
                new AntilFile
                {
                    Name = "1",
                    Path = "1",
                    Updated = DateTime.Now,
                    Version = 1
                },
                new AntilFile
                {
                    Name = "2",
                    Path = "2",
                    Updated = DateTime.Now,
                    Version = 1
                },
                new AntilFile
                {
                    Name = "3",
                    Path = "3",
                    Updated = DateTime.Now,
                    Version = 1
                }
            };

            InstallContainer();

            var antilFileDao = IOC.Resolve<IAntilFileDao>();
            antilFileDao.BulkSave(fileList);

            var names = antilFileDao.CreateQuery().Select(x => x.Name);
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }

            Console.Write("\ndone!\n");
            Console.ReadKey();
        }

        private static void InstallContainer()
        {
            IOC = new WindsorContainer();
            IOC.Install(new WindsorNhibernateInstaller());
        }
    }
}
