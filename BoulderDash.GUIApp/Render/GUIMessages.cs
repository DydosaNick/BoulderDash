using BoulderDash.Core.Utilites;
using BoulderDash.Core.GameObjects.Entities;
using System.Collections.Generic;
using System.Drawing;

namespace BoulderDash.GUIApp.Render
{
    public class GUIMessages
    {
        public Dictionary<GameStates, (Color, string)> MessagesDictionary { get; private set; }

        public GUIMessages()
        {
            MessagesDictionary = new Dictionary<GameStates, (Color, string)>()
            {
                { GameStates.NotStarted, (Color.White, "Welcome to Boulder Dash! Select a level to start.") },
                { GameStates.Menu, (Color.Yellow, "Welcome! Please enter a level number and click Start.") },
                { GameStates.Playing, (Color.Green, "Game is running...") },
                { GameStates.LevelCompleted, (Color.Cyan, "Level completed! Press Escape to go to menu or continue to next level.") },
                { GameStates.GameCompleted, (Color.Magenta, $"Congratulations! You've completed all levels! Your final score: {Player.DiamondScore}") },
                { GameStates.InvalidInput, (Color.Red, "Invalid level! Please enter a valid number.") },
                { GameStates.PlayerDied, (Color.Red, "You died! Press Escape to return to menu.") },
                { GameStates.PlayerExitByHimself, (Color.Orange, "You exited the game! Press Escape to return to menu.") },
                { GameStates.NoEscape, (Color.DarkRed, "You can't escape from this level. Press Escape to return to menu.") }
            };
        }

        public void UpdateDynamicMessages()
        {
            MessagesDictionary[GameStates.GameCompleted] = (Color.Magenta, $"Congratulations! You've completed all levels! Your final score: {Player.DiamondScore}");
        }
    }
}
