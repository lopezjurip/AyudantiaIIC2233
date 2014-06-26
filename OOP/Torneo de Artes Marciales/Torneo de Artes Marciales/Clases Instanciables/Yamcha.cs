using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torneo_de_Artes_Marciales
{
    class Yamcha: Humano
    {
        public Yamcha()
            : base("Yamcha")
        {
            hp = 100000;
            ki = 10;
        }

        public new bool AtacarYMatar(Personaje p)
        {
            Console.WriteLine("Yamcha es muy penca y no hace daño");
            return false;
        }
        public override bool RecibirDanoYMuere(int Dano)
        {
            Console.WriteLine("Yamcha muere de inmediato");
            MorirDeInmediato();
            return true;
        }
    }
}
