using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.Utilites
{
    public enum GameStates
    {
        NotStarted,
        Menu,
        Playing,
        PlayerDied,
        LevelCompleted,
        GameCompleted,
        PlayerExitByHimself,
        InvalidInput,
        NoEscape
    }
}
