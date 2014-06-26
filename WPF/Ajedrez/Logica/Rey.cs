using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    class Rey : PiezaAjedrez
    {
        public Rey(string color) : base("Rey", color) { }

        public override bool Movimiento(int Xi, int Yi, int Xf, int Yf)
        {
            if (Math.Abs(Xi - Xf) <= 1 &&  (Math.Abs(Yi - Yf) <= 1))
                return true;
            return false;
        }
    }
}
