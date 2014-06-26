using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo_de_Artes_Marciales
{
    // Para implementar rápidamente una interfaz, segundo click sobre la interfaz, en este caso a "iSaiyajin" y presionar Implementar Interfaz
    class Goku : Saiyajin // <- Segundo click aquí
    {
        public Goku(): base("Goku")
        {
            hp = 1000;
            ki = Program.GeneradorNumerosRandom.Next(0, 200);
        }

        public override bool AtacarYMatar(Personaje p)
        {
            ConseguirEnergiaVital();
            return p.RecibirDanoYMuere(this.PoderDelAtaque());
        }

        private int PoderDelAtaque()
        {
            // Intenta transformarse en SuperSaiyajin.
            if (isSuperSaiyajin)
                return this.Kakamehameha();
            else if (IntentarTransformarEnSuperSaiyajin())
                return this.Kakamehameha();

            return Fuerza;
        }

        public override bool RecibirDanoYMuere(int Dano)
        {
            bool GokuMuere = base.RecibirDanoYMuere(Dano);
            if (GokuMuere == true)
                if (Program.GeneradorNumerosRandom.Next(0, 100) <= 10) // Goku tiene la probabilidad de un 10% de revivir.
                {
                    Console.WriteLine(Nombre +" ha revivido!");
                    hp = 900;
                    ki = 1000;
                    GokuMuere = false;
                }
            return GokuMuere;
        }

        private void ConseguirEnergiaVital()
        {
            ki += 50;
            hp += 50;
        }
    }
}
