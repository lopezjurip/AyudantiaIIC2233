using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    // Necesitamos una clase abstracta que tenga los campos: Tipo y Color.
    // Además, un método abstracto validador de movimientos que reciba 2 coordenadas y devuelva un bool.
              
    #region Solución clase abstracta
    public abstract class PiezaAjedrez
    {
        public string Tipo, Color;
        public PiezaAjedrez(string tipo, string color)
        {
            Tipo = tipo;
            Color = color;
        }
        public abstract bool Movimiento(int Xi, int Yi, int Xf, int Yf);
    }
    #endregion
}
