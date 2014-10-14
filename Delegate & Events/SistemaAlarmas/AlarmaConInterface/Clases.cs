using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmaConInterface
{
    /* Cuando suene una alarma (llamar método Sonar()) en consola se verá:
     * RING!
     * AHHHHHH!
     * RING RING!
     */

    /// <summary>
    /// Interface que deben implementar los que quieran saber qué sucede con la alarma.
    /// </summary>
    interface EscuchadorDeAlarmas
    {
        void HaSonado();
    }

    /// <summary>
    /// En algún momento sonará.
    /// </summary>
    class Alarma
    {
        public EscuchadorDeAlarmas escuchador;

        public void Sonar()
        {
            Console.WriteLine("RING!");
            if (escuchador != null)
                escuchador.HaSonado();
            Console.WriteLine("RING RING!");
        }
    }

    class Campus : EscuchadorDeAlarmas
    {
        protected Alarma[] alarmas;
        protected Seguridad seguridad;

        public void Suscribirse()
        {
            foreach (Alarma a in alarmas)
                a.escuchador = this; // seguridad; porque podría cualquiera clase que implemente la interface.
        }

        public void HaSonado()
        {
            Console.WriteLine("AHHHHHH!");
        }
    }

    /// <summary>
    /// Clase cualquiera que también puede enterarse de la alarma.
    /// </summary>
    class Seguridad : EscuchadorDeAlarmas
    {
        public void HaSonado()
        {
            Console.WriteLine("ALERTA!");
        }
    }
}
