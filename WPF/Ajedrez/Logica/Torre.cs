using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    class Torre:PiezaAjedrez
    {
        public Torre(string color) : base("Torre", color) { }

        public override bool Movimiento(int Xi, int Yi, int Xf, int Yf)
        {
            if (Xi == Xf)
                return true;
            else if (Yi == Yf)
                return true;
            return false;
        }
    }
}
