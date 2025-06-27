using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.GameObjects.Cells
{
    public class Diamond : Cell
    {
        public Diamond(int x, int y) : base(x, y)
        {
            IsPassibleByPlayer = true;
        }

    }
}
