using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class @string
    {
        public string Identidad { get; set; }
        public string Nombre { get; set; }

        public @string()
        {
        }

        public @string(string identidad, string nombre)
        {
            Identidad = identidad;
            Nombre = nombre;
        }
    }
}
