using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logic;
using ServiceStack.Common;

namespace Repositorio
{
    public class RepositoryTxt<T> : IRepository<T>
    {
        public  T Añadir(T entity)
        {
            try
            {
                var student =entity as Student;
                using (StreamWriter fl = new StreamWriter("../Student.txt",true))
                {
                    fl.WriteLine(student.ToString());
                }
                object entidad;
                var props = new object[]{ student.Edad,student.Nombre,student.Apellidos};
                entidad =Activator.CreateInstance(typeof(T), props);
                return (T)entidad;
            }
            catch (IOException ex)
            {
                throw new RepositoryException("Error en la capa Repositorio",ex);
            }
        }

        public  T Leer()
        {
            try
            {
                Student student = new Student();
                string Linea;
                using (StreamReader fl = new StreamReader("../Student.txt",true))
                {
                    while ((Linea= fl.ReadLine())!=null)
                    {
                        var propiedades = Linea.Split(',');
                        student = new Student(Convert.ToInt32(propiedades[0]),propiedades[1],propiedades[2]);
                    }
                    object entidad;
                    var props = new object[] { student.Edad, student.Nombre, student.Apellidos };
                    entidad = Activator.CreateInstance(typeof(T), props);
                    return (T)entidad;
                }
            }
            catch (FileNotFoundException ex)
            {

                throw new RepositoryException("Archivo no encontrado",ex);
            }
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                return await Task.Run(()=>Añadir(entity));
            }
            catch (RepositoryException ex)
            {
                throw new RepositoryException("Excepcion en el repositorio",ex);
            };
        }

        public async Task<T> Get()
        {
            try
            {
                return await Task.Run(() => Leer());
            }
            catch (RepositoryException ex)
            {
                throw new RepositoryException("Excepcion en el repositorio", ex);
            };
        }
    }
}
