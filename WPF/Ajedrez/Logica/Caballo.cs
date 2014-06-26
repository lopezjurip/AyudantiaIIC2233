using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    class Caballo: PiezaAjedrez
    {
        public Caballo(string color) : base("Caballo", color) { }

        public override bool Movimiento(int Xi, int Yi, int Xf, int Yf)
        {
            int a = Math.Abs(Xf - Xi);
            int b = Math.Abs(Yf - Yi);
            if (a * a + b * b == 5) // Pitágoras
                return true;
            return false;
        }
    }
}