using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logic
{
    public class Student
    {
        public int Edad { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }

        public Student() { }

        public Student(int edad , string nombre , string apellido)
        {
            Edad = edad;Nombre = nombre;Apellidos = apellido;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{Edad} , {Nombre} , {Apellidos}");
            return sb.ToString();
        }
    }
}
