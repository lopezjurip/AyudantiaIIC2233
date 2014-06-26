using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    class Peon: PiezaAjedrez
    {
        public Peon(string color) : base("Peon", color) { }

        public override bool Movimiento(int Xi, int Yi, int Xf, int Yf)
        {
            if (Math.Abs(Xi - Xf) <= 1)
            {
                if (Color.Equals("Blanco") && Yi - Yf == 1)
                    return true;
                else if (Color.Equals("Negro") && Yf - Yi == 1)
                    return true;
            }
            return false;
        }
    }
}