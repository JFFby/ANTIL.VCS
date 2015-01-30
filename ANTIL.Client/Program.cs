using System;
using System.Xml;
using ANTIL.Domain;
using ANTIL.Domain.Entities;
using ANTIL.Domain.Repositoies;

namespace ANTIL.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = new AntilFile
            {
                Name = "test",
                Path = "/",
                Updated = DateTime.Now,
                Version = 1
            };

            var repositiry = new AntilFileRepository();
            repositiry.Save(file);

            Console.Write("done!\n");
            Console.ReadKey();
        }
    }
}
