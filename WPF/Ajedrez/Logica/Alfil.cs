using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    class Alfil: PiezaAjedrez
    {
        public Alfil(string color) : base("Alfil", color) { }

        public override bool Movimiento(int Xi, int Yi, int Xf, int Yf)
        {
            if (Math.Abs(Xf - Xi) == Math.Abs(Yi - Yf))
                return true;
            return false;
        }
    }
}