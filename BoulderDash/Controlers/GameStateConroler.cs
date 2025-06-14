using BoulderDash.Core.GameObjects.Entities;
using BoulderDash.Core.Utilites;
using BoulderDash.Core.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.Controlers
{
    public class GameStateConroler
    {
        private GameStates CurrentGameState { get; set; } = GameStates.NotStarted;

        public void ControlGameState(GameCore gameCore)
        {
            bool isGameRunning = true;

            while (isGameRunning)
            {
                GameWorld gameWorld = gameCore.gameWorld;

                switch (CurrentGameState)
                {
                    case GameStates.NotStarted:
                        gameWorld.CheckMaxLevel();
                        CurrentGameState = GameStates.Menu;
                        break;
                    case GameStates.Menu:
                        string tempStr = gameCore.gameInput.MenuInput(CurrentGameState);
                        if (int.TryParse(tempStr, out int level) && level > 0 && level <= gameWorld.MaxLevel)
                        {
                            gameWorld.CurrentLevel = level;
                        }
                        else
                        {
                            CurrentGameState = GameStates.InvalidInput;
                            continue;
                        }

                        gameWorld.LoadCurrentLevel();
                        CurrentGameState = GameStates.Playing;
                        break;
                    case GameStates.Playing:
                        gameCore.GameLoop();
                        break;
                    case GameStates.LevelCompleted:

                        if (gameWorld.CurrentLevel >= gameWorld.MaxLevel)
                        {
                            ChangeGameState(GameStates.GameCompleted);
                        }
                        else
                        {
                            gameCore.gameRender.ShowMessage(CurrentGameState);
                            gameWorld.LoadNextLevel();
                            ChangeGameState(GameStates.Playing);
                        }
                        break;
                    case GameStates.InvalidInput:
                    case GameStates.NoEscape:
                    case GameStates.PlayerDied:
                    case GameStates.GameCompleted:
                    case GameStates.PlayerExitByHimself:
                        Thread.Sleep(300);
                        gameCore.gameRender.ShowMessage(CurrentGameState);
                        Thread.Sleep(500);
                        isGameRunning = false;
                        break;
                    default:
                        return;
                }
            }
        }

        public void EvaluateGameStateActions(Actions actions)
        {
            if (!Player.IsPlayerAlive)
            {
                ChangeGameState(GameStates.PlayerDied);
            }
            
            switch(actions)
            {
                case Actions.LeaveGame:
                    ChangeGameState(GameStates.PlayerExitByHimself);
                    break;
            }
        }

        internal void ChangeGameState(GameStates gameState)
        {
            CurrentGameState = gameState;
        }
    }
}
