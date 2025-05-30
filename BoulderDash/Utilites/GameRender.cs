using BoulderDash.Core.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.Utilites
{
    public abstract class GameRender
    {
        public abstract void Render(GameWorld gameMap);
        public abstract void ShowMessage(GameStates gameStates);
    }
}
