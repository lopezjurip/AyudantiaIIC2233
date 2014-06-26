using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cola_Almuerzo
{
    class MicroondaQueue : AlmuerzoQueue
    {
        private float tolerancia;
        private Random RANDOM;

        public MicroondaQueue(float tolerancia) : base()
        {
            this.tolerancia = tolerancia;
            RANDOM = new Random();
        }

        // Como NO es virtual ni abstract, debo usar "new"
        public new void Colar(Persona colado, Persona colador)
        {
            if (RANDOM.Next(0, 101) < tolerancia * 100)
                base.Colar(colado, colador);
            else
                base.Queue(colado);
            // #iLoveHerencia
        }
    }
}
