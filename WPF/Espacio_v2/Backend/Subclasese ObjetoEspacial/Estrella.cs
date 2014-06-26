using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    class Estrella : ObjetoEspacial
    {
        private double constante;
        private double pendiente;

        public Estrella(Random generador)
        {
            imagenNombre = "star.png";
            constante = generador.Next(-10, 10) * 0.0001;
            pendiente = generador.Next(-100, 100) * 0.00001;
            w = h = generador.Next(5, 30);
        }

        public override void Moverse(double cantidad)
        {
            x -= cantidad;
            y += pendiente * cantidad + constante;

            llamarEventoCambioCoordenadas(x, y);
        }
    }
}
