using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Backend
{
    public class ApocalipsisZombieSolucion : ApocalipsisZombie
    {
        public ManualResetEvent noHayPeligroEnCrearZombie = new ManualResetEvent(true);

        public ApocalipsisZombieSolucion(int tamanoGrid) : base(tamanoGrid) { }

        protected override Zombie CrearZombie()
        {
            Zombie z;
            lock (zombiesActuales)
            {
                noHayPeligroEnCrearZombie.Reset();
                z = base.CrearZombie();
                noHayPeligroEnCrearZombie.Set();
            }
            return z;
        }

        protected override bool z_hayUnZombieEnEsaCoordenada(Coords arg)
        {
            bool hayZombie = true;
            lock (zombiesActuales)
            {
                noHayPeligroEnCrearZombie.WaitOne();
                hayZombie = base.z_hayUnZombieEnEsaCoordenada(arg);
            }
            return hayZombie;
        }
    }
}
