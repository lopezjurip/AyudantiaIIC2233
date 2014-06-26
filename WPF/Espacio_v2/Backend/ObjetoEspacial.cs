using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public abstract class ObjetoEspacial
    {
        /* El objeto espacial se mueve */
        public event Action<double, double> cambioDeCoordenadas;

        /* Morirá */
        public event Action seraBorrado;

        /* Otras propiedades */
        protected double x, y, w, h;
        protected String imagenNombre;

        /* getters */
        public double X
        {
            get { return x; }
        }
        public double Y
        {
            get { return y; }
        }
        public double W
        {
            get { return w; }
        }
        public double H
        {
            get { return h; }
        }
        public String ImagenNombre
        {
            get { return imagenNombre; }
        }

        // Cada subclase debe implementarlo
        // Es tan burda la manera en que cada uno lo implementa que no hay que preocuparse por eso y solo aceptarlo pues fucniona :)
        public abstract void Moverse(double cantidad);

        // Cosas tontas de NET que no nos deja gatillar el evento directamente desde la subclase ¬¬
        protected void llamarEventoCambioCoordenadas(double x, double y)
        {
            if(cambioDeCoordenadas != null)
                cambioDeCoordenadas(x, y);
        }

        // Se usa despues de nacer (constructor)
        public void setInicio(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // Avisa que sera borrado.
        public void prepararParaBorrar()
        {
            if (seraBorrado != null)
                seraBorrado();
        }
    }
}
