using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.GameObjects.Entities;
using BoulderDash.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.ConsoleApp.Render
{
    public class Messages
    {
        //Messages Dictionary
        public Dictionary<GameStates, (ConsoleColor, string)> MessagesDictionary
        {
            get
            {
                {
                    return new Dictionary<GameStates, (ConsoleColor, string)>()
                    {
                        { GameStates.NotStarted, (ConsoleColor.White, "Welcome to Boulder Dash! Press any key to start...") },
                        { GameStates.Menu, (ConsoleColor.Yellow,"Welcome") },
                        { GameStates.Playing, (ConsoleColor.Green, "Game is running...") },
                        { GameStates.LevelCompleted, (ConsoleColor.Cyan, "Level completed! Press any key to continue...") },
                        { GameStates.GameCompleted, (ConsoleColor.Magenta, "Congratulations! You've completed all levels!") },
                        { GameStates.InvalidInput, (ConsoleColor.Red, "Invalid input! Please try again.") },
                        { GameStates.PlayerDied, (ConsoleColor.Red, "You died! Press any key to continue...") },
                        { GameStates.PlayerExitByHimself, (ConsoleColor.Cyan, "You exited the game! Press any key to continue...") },
                        { GameStates.NoEscape, (ConsoleColor.Red, "You can't escape from this level. You lose! Press any key to continue...") }
                    };
                }
            }
        }
    }
        
}
