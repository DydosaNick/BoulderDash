using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.GameObjects.Cells
{
    public class Air : Cell
    {
        public Air(int x, int y) : base(x, y)
        {
            IsPassible = true;
            IsPassibleByPlayer = true;
        }

    }
}
