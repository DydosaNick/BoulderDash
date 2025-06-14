using BoulderDash.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.ConsoleApp.Input
{
    public class Keys
    {
        public Dictionary<ConsoleKey, Actions> KeyBindings
        {
            get
            {
                return new Dictionary<ConsoleKey, Actions>()
                {
                    { ConsoleKey.W, Actions.MoveUp },
                    { ConsoleKey.UpArrow, Actions.MoveUp },
                    { ConsoleKey.S, Actions.MoveDown },
                    { ConsoleKey.DownArrow, Actions.MoveDown },
                    { ConsoleKey.A, Actions.MoveLeft },
                    { ConsoleKey.LeftArrow, Actions.MoveLeft },
                    { ConsoleKey.D, Actions.MoveRight },
                    { ConsoleKey.RightArrow, Actions.MoveRight },
                    { ConsoleKey.Enter, Actions.Interact },
                    { ConsoleKey.Escape, Actions.LeaveGame }
                };
            }
        }
    }
}
