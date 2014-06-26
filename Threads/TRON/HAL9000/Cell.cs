using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL9000
{
    public class Cell // Estructura de datos, lo recuerdan? 
    {
        public Cell next = null;
        public Cell Izquierda, Derecha, Arriba, Abajo;
        public Coord XY;

        public Cell(Coord c)
        {
            XY = c;
        }


    }
}
