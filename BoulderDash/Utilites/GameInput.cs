using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.Utilites
{
    public abstract class GameInput
    {
        public abstract Actions HandleInput();

        public virtual string MenuInput() { return "1"; }


    }
}
