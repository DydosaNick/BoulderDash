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
            if(gameCore.gameRender.Id == 'C')
            {
                while (true)
                {
                    Control(gameCore);

                    switch(CurrentGameState)
                    {
                        case GameStates.InvalidInput:
                        case GameStates.NoEscape:
                        case GameStates.PlayerDied:
                        case GameStates.GameCompleted:
                        case GameStates.PlayerExitByHimself:
                            gameCore.gameRender.ShowMessage(CurrentGameState);
                            return;
                    }
                }
            }
            else if (gameCore.gameRender.Id == 'G')
            {
                Control(gameCore);
                switch(CurrentGameState)
                {
                    case GameStates.InvalidInput:
                    case GameStates.NoEscape:
                    case GameStates.PlayerDied:
                    case GameStates.GameCompleted:
                    case GameStates.PlayerExitByHimself:
                        Environment.Exit(0);
                        return;
                }
            }
            else
            {
                throw new NotSupportedException("Unsupported game render type.");
            }


        }

        private void Control(GameCore gameCore)
        {
            GameWorld gameWorld = gameCore.gameWorld;

            switch (CurrentGameState)
            {
                case GameStates.NotStarted:
                    gameWorld.CheckMaxLevel();
                    gameWorld.CheckMaxDiamonds();
                    CurrentGameState = GameStates.Menu;
                    break;
                case GameStates.Menu:
                    string tempStr = gameCore.gameInput.MenuInput();
                    if (int.TryParse(tempStr, out int level) && level > 0 && level <= gameWorld.MaxLevel)
                    {
                        gameWorld.CurrentLevel = level;
                    }
                    else
                    {
                        CurrentGameState = GameStates.InvalidInput;
                        return;
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
                default:
                    return;
            }
        }

        public void EvaluateGameState(Actions actions)
        {
            if (!Player.IsPlayerAlive)
            {
                ChangeGameState(GameStates.PlayerDied);
            }

            switch (actions)
            {
                case Actions.LeaveGame:
                    ChangeGameState(GameStates.PlayerExitByHimself);
                    break;
            }
        }

        public void ChangeGameState(GameStates gameState)
        {
            CurrentGameState = gameState;
        }

        public GameStates GetCurrentGameState() { return CurrentGameState; }
    }
}