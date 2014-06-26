using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo_de_Artes_Marciales
{
    class Vegeta : Saiyajin
    {
        public Vegeta()
            : base("Vegeta")
        {
            hp = 1100;
            ki = Program.GeneradorNumerosRandom.Next(0, 150);
        }

        public override bool AtacarYMatar(Personaje p)
        {
            return p.RecibirDanoYMuere(PoderDelAtaque());
        }

        private int PoderDelAtaque()
        {
            if (isSuperSaiyajin)
                return this.Kakamehameha();
            else if (IntentarTransformarEnSuperSaiyajin())
                return this.Kakamehameha();

            return Fuerza + this.Ira(); // Vegeta puede aumentar su poder.
        }

        private int Ira()
        {
            ki += 100;
            return Program.GeneradorNumerosRandom.Next(0, 100);
        }
    }
}
