using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.GameObjects.Cells
{
    public class Exit : Cell
    {
        public Exit(int x, int y) : base(x, y)
        {
            IsPassibleByPlayer = true;
        }


    }


}
