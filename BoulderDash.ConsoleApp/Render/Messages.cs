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
        public Dictionary<GameStates, string> MessagesDictionary
        {
            get
            {
                {
                    return new Dictionary<GameStates, string>()
                    {
                        { GameStates.NotStarted, "Game not started yet. Press any key to start." },
                        { GameStates.Menu, "Welcome to Boulder Dash!" },
                        { GameStates.Playing, "Game is in progress. Good luck!" },
                        { GameStates.PlayerDied, "You died! Press 'R' to restart or 'Q' to quit." },
                        { GameStates.LevelCompleted, "Level completed!" },
                        { GameStates.GameCompleted, "Congratulations! You've completed the game!" },
                        { GameStates.PlayerExitByHimself, "You exited the game by yourself." },
                        { GameStates.InvalidInput, "Invalid input! Please try again." },
                        { GameStates.NoEscape, "No escape available at this moment." }
                    };
                }
            }
        }
    }
        
}
