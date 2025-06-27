using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.GameObjects.Entities;
using BoulderDash.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.GUIApp.Input
{
    public class GUIKeys
    {
        public Dictionary<Keys, Actions> Key = new()
        {
            { Keys.W, Actions.MoveUp },
            { Keys.A, Actions.MoveLeft},
            { Keys.E,  Actions.Interact},
            { Keys.S, Actions.MoveDown},
            { Keys.D, Actions.MoveRight },
            { Keys.Q, Actions.LeaveGame },
            { Keys.P, Actions.Pause },
        };
    }
}
