using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cola_Almuerzo
{
    class AlmuerzoQueueSeguridad : AlmuerzoQueue
    {
        // Sin construcor por flojera
        Dictionary<Persona, Persona> coladosPillados = new Dictionary<Persona, Persona>();

        // Como NO es virtual ni abstract, debo usar "new"
        public new void Colar(Persona colado, Persona colador)
        {
            // Con esto mantenemos registro que quien fue el que dejó que se colaran
            coladosPillados.Add(colado, colador);
            base.Colar(colado, colador);

            /* 
             * Por qué no:
             * coladosPillados.Add(colador, colado); ?
             * Por que si alguien cola a más de uno se pierde la referencia al primero
             */

            /* Aún así tiene un error, ¿qué pasa si alguien es colado dos veces ? */
        }

        public override string ToString()
        {
            String s = "";
            foreach (KeyValuePair<Persona, Persona> pp in coladosPillados)
                s = s + "Colado: " +pp.Key +" | Colador: "+pp.Value +"\n";
            s = s + "-------------------------\n";
            
            return s + base.ToString();
        }
    }
}
