using BoulderDash.Core.GameObjects;
using BoulderDash.Core.GameObjects.Cells;
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
    public class PlayerControler
    {
        private int MaxTicsUnderDanger { get; set; } = 20;

        private int CurrentTicsUnderDanger { get; set; } = 0;

        private int InteractionRange { get; set; } = 1;

        public void PlayerAction(Actions key, GameWorld gameWorld, GameStateConroler gameStateConroler)
        {
            Vector2D movementVector = new Vector2D(0, 0);
            switch (key)
            {
                case Actions.MoveUp:
                    movementVector.Y = -1;
                    MovePlayer(movementVector, gameWorld, gameStateConroler);
                    break;
                case Actions.MoveDown:
                    movementVector.Y = 1;
                    MovePlayer(movementVector, gameWorld, gameStateConroler);
                    break;
                case Actions.MoveLeft:
                    movementVector.X = -1;
                    MovePlayer(movementVector, gameWorld, gameStateConroler);
                    break;
                case Actions.MoveRight:
                    movementVector.X = 1;
                    MovePlayer(movementVector, gameWorld, gameStateConroler);
                    break;
                case Actions.Interact:
                    Interact(gameWorld);
                    break;
                case Actions.LeaveGame:
                    gameStateConroler.ChangeGameState(GameStates.PlayerExitByHimself);
                    break;
                case Actions.Nothing:
                    break;
            }            

            CheckPlayerSafety(gameWorld);
        }

        private void Interact(GameWorld gameWorld)
        {
            for (int i = Player.CurrentPlayerPosition.X - InteractionRange; i <= Player.CurrentPlayerPosition.X + InteractionRange; i++)
            {
                for (int j = Player.CurrentPlayerPosition.Y - InteractionRange; j <= Player.CurrentPlayerPosition.Y + InteractionRange; j++)
                {
                    if (i < 0 || i >= gameWorld.Width || j < 0 || j >= gameWorld.Height)
                    {
                        continue;
                    }

                    if (gameWorld.Map[i,j] is Entity entity && entity.IsInteractionaable)
                    {
                        switch(entity)
                        {
                            case Rock rock:
                                // Implement rock interaction logic here
                                break;
                            case Bomb bomb:
                                bomb.isExplousionActive = true;
                                break;
                            case Block enemy:
                                // Implement enemy interaction logic here
                                break;
                            default:
                                // Handle other entities if needed
                                break;
                        }
                    }
                }
            } 
        }

        private void MovePlayer(Vector2D movementVector, GameWorld gameWorld, GameStateConroler gameStateConroler)
        {
            Vector2D playerPosition =  Player.CurrentPlayerPosition;

            Vector2D newPlayerPosition = new Vector2D
                (playerPosition.X + movementVector.X,
                playerPosition.Y + movementVector.Y);

            if (newPlayerPosition.X < 0 || newPlayerPosition.X >= gameWorld.Width ||
                newPlayerPosition.Y < 0 || newPlayerPosition.Y >= gameWorld.Height)
            {
                return;
            }
            else if (gameWorld.Map[newPlayerPosition.X, newPlayerPosition.Y] is Exit)
            {
                gameStateConroler.ChangeGameState(GameStates.LevelCompleted);
                return;
            }
            else if (gameWorld.Map[newPlayerPosition.X, newPlayerPosition.Y] is Entity entity && entity.IsMoveable)
            {
                Vector2D entityNewPosition = new Vector2D(newPlayerPosition.X + movementVector.X, newPlayerPosition.Y + movementVector.Y);
                
                if(gameWorld.Map[entityNewPosition.X, entityNewPosition.Y] is Air)
                {
                    if(gameWorld.Map[newPlayerPosition.X, newPlayerPosition.Y] is Block || movementVector.Y != -1)
                    { 
                        gameWorld.Map[entityNewPosition.X, entityNewPosition.Y] = gameWorld.Map[newPlayerPosition.X, newPlayerPosition.Y];
                        gameWorld.Map[entityNewPosition.X, entityNewPosition.Y].Position = entityNewPosition;

                        gameWorld.Map[newPlayerPosition.X, newPlayerPosition.Y] = gameWorld.Map[playerPosition.X, playerPosition.Y];
                        Player.CurrentPlayerPosition = newPlayerPosition;

                        gameWorld.Map[playerPosition.X, playerPosition.Y] = new Air(playerPosition.X, playerPosition.Y);
                    }
                } 
            }
            else if (gameWorld.Map[newPlayerPosition.X, newPlayerPosition.Y].IsPassibleByPlayer)
            {
                if (gameWorld.Map[newPlayerPosition.X, newPlayerPosition.Y] is Diamond)
                {
                    Player.DiamondScore++;
                }

                gameWorld.Map[newPlayerPosition.X, newPlayerPosition.Y] = gameWorld.Map[playerPosition.X, playerPosition.Y];
                gameWorld.Map[playerPosition.X, playerPosition.Y] = new Air(playerPosition.X, playerPosition.Y);
                Player.CurrentPlayerPosition = newPlayerPosition;
            }             
        }

        private void CheckPlayerSafety(GameWorld gameWorld)
        {
            Vector2D playerPosition = Player.CurrentPlayerPosition;
            Player.IsPlayerUnderDanger = false;

            if (gameWorld.Map[playerPosition.X, playerPosition.Y - 1] is Rock)
            {
                Player.IsPlayerUnderDanger = true;
            }

            if (Player.IsPlayerUnderDanger)
            {
                CurrentTicsUnderDanger++;
            }

            if (CurrentTicsUnderDanger >= MaxTicsUnderDanger)
            {
                Player.IsPlayerAlive = false;
            }
        }

    }
}
