using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public abstract class ObjetoEspacial
    {
        /// <summary>
        /// Gatillado cuando el objeto se ha movido
        /// </summary>
        public event Action<double, double> CambioDeCoordenadas;

        /// <summary>
        /// Gatillado cuando el objeto debe morir.
        /// </summary>
        public event Action SeraBorrado;

        /// <summary>
        /// Posición en eje X (izquierda a derecha)
        /// </summary>
        public double X { get; protected set; }

        /// <summary>
        /// Posición en eje Y (arriba a abajo)
        /// </summary>
        public double Y { get; protected set; }

        /// <summary>
        /// Largo del objeto
        /// </summary>
        public double W { get; protected set; }

        /// <summary>
        /// Altura del objeto
        /// </summary>
        public double H { get; protected set; }

        /// <summary>
        /// El nombre de archivo y extensión.
        /// </summary>
        public abstract String NombreImagen { get; }
          
        internal ObjetoEspacial(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        // Cada subclase debe implementarlo
        // Es tan burda la manera en que cada uno lo implementa que no hay que preocuparse por eso y solo aceptarlo pues fucniona :)
        public abstract void Moverse(double cantidad);

        // Cosas tontas de NET que no nos deja gatillar el evento directamente desde la subclase ¬¬
        protected void GatillarCambioCoordenadas(double x, double y)
        {
            if(CambioDeCoordenadas != null)
                CambioDeCoordenadas(x, y);
        }

        // Avisa que sera borrado.
        public void PrepararParaBorrar()
        {
            if (SeraBorrado != null)
                SeraBorrado();
        }
    }
}
