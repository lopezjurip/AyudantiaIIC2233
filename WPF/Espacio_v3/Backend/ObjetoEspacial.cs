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
        /// Tiempo por defecto que demorar en girar 360º.
        /// </summary>
        public const int DEFAULT_ROTATION_TIME = 3000;

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
        /// Cuantos milisegundos demora en girar 360º.
        /// </summary>
        public double TiempoRotacion { get; protected set; }

        /// <summary>
        /// El nombre de archivo y extensión.
        /// </summary>
        public abstract String NombreImagen { get; }
          
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="X">Posición inicial en X</param>
        /// <param name="Y">Posición inicial en Y</param>
        public ObjetoEspacial(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
            TiempoRotacion = DEFAULT_ROTATION_TIME;
        }

        /// <summary>
        /// Cada subclase debe implementarlo. 
        /// </summary>
        /// <param name="cantidad">Un cierto valor relativo.</param>
        public abstract void Moverse(double cantidad);


        /// <summary>
        /// Cosas tontas de NET que no nos deja gatillar el evento directamente desde la subclase ¬¬
        /// </summary>
        /// <param name="x">Mov. en eje X</param>
        /// <param name="y">Mov. en eje Y</param>
        protected void GatillarCambioCoordenadas(double x, double y)
        {
            if(CambioDeCoordenadas != null)
                CambioDeCoordenadas(x, y);
        }


        /// <summary>
        /// Avisa que sera borrado.
        /// </summary>
        public void PrepararParaBorrar()
        {
            if (SeraBorrado != null)
                SeraBorrado();
        }
    }
}
