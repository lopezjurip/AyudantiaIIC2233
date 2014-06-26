using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    class Reina: PiezaAjedrez
    {
        public Reina(string color) : base("Reina", color) { }

        public override bool Movimiento(int Xi, int Yi, int Xf, int Yf)
        {
            if (Xi == Xf)
                return true;
            else if (Yi == Yf)
                return true;
            else if (Math.Abs(Xf - Xi) == Math.Abs(Yf - Yi))
                return true;
            else
                return false;
        }
    }
}
