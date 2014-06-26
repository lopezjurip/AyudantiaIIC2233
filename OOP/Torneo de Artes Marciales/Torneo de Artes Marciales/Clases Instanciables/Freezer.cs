using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo_de_Artes_Marciales
{
    class Freezer: Personaje, iMalvado
    {
        int Nivel = 1;

        iMalvado hola = new Freezer();

        public Freezer()
            : base("Freezer")
        {
            hp = 1200;
            ki = Program.GeneradorNumerosRandom.Next(0, 2000);
        }

        public override bool AtacarYMatar(Personaje p)
        {
            CambiarForma();
            return p.RecibirDanoYMuere(Fuerza * Nivel);
        }

        public void DestuirElMundo()
        {
            Console.WriteLine("El gran freezer destruyó la tierra");
        }

        public bool estaVivo()
        {
            return true;
        }

        private void CambiarForma()
        {
            if (Nivel == 4) // Solo puede llegar hasta Nivel 4.
                return;

            if (ki >= 2000)
            {
                Nivel += 1;
                ki -= 2000;
                Console.WriteLine(Nombre + " ha alcanzado su forma #" + Nivel);
            }
            else
                ki += 200;
            
        }
    }
}
