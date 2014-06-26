using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cola_Almuerzo
{
    class Persona
    {
        private string Nombre;
        public string nombre { get { return Nombre; } }

        public Persona(string Nombre)
        {
            this.Nombre = Nombre;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
