using Common.Logic;
using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Repositorio;

namespace Redis.Task.Autofac
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var alumno1 = new Student
                {
                    Edad = 14,
                    Nombre = "Biel",
                    Apellidos = "Sanchez"
                };
                var alumno2 = new Student
                {
                    Edad = 14,
                    Nombre = "Biel",
                    Apellidos = "Sanchez"
                };
                //utilizamos autofac para crear las intancias
                var builder = new ContainerBuilder();
                    builder.RegisterType<RepositoryRedis<Student>>().As<IRepository<Student>>();
                    builder.RegisterType<RepositoryRedis<Student>>().As<IRepository<Student>>();
                var container = builder.Build();
                //inyectamos las instancias creadas por autofac
                IBusiness<Student> Bl = new BusinessProyect<Student>(container.Resolve<IRepository<Student>>());

                var alumnoIgresado1 = Bl.Add(alumno1);
                alumnoIgresado1.Wait();
                Console.WriteLine(alumnoIgresado1.Result.ToString());
                var alumnoDevuelto1 = Bl.Read();
                alumnoDevuelto1.Wait();
                Console.WriteLine(alumnoDevuelto1.Result.ToString());

                Bl = new BusinessProyect<Student>(container.Resolve<IRepository<Student>>());
                var alumnoIgresado2 = Bl.Add(alumno2);
                alumnoIgresado2.Wait();
                Console.WriteLine(alumnoIgresado2.Result.ToString());
                var alumnoDevuelto2 = Bl.Read();
                alumnoDevuelto2.Wait();
                Console.WriteLine(alumnoDevuelto2.Result.ToString());

            }
            catch (BusinessException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
