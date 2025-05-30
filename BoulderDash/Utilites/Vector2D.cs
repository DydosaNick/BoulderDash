using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.Utilites
{
    public class Vector2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2D()
        {
        }
        public Vector2D(int x, int y)
        {

            X = x;
            Y = y;
        }
    }
}
