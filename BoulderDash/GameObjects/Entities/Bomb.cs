using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.Utilites;
using BoulderDash.Core.Utilites.Interfaces;
using BoulderDash.Core.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.GameObjects.Entities
{
    public class Bomb : Entity, IExplode
    {
        public Bomb(int x, int y) : base(x, y)
        {
            IsInteractionaable = true;
        }

        public bool isExplousionActive = false;

        private readonly int maxTicksBeforeExplousion = 30;

        private int currentTickBeforeExplosion = 0;

        private readonly int maxTicksBeforeFalling = 4;

        private int currentTickBeforeFalling = 0;

        private readonly int radiusOfExplousion = 2;

        public override void React(GameWorld gameWorld, Vector2D position)
        {
            GameObject[,] gameMap = gameWorld.Map;

            Vector2D leftPos = new Vector2D(position.X - 1, position.Y);
            Vector2D rightPos = new Vector2D(position.X + 1, position.Y);
            Vector2D downPos = new Vector2D(position.X, position.Y + 1);
            Vector2D newLeftDownPosition = new Vector2D(position.X - 1, position.Y + 1);
            Vector2D newRightDownPosition = new Vector2D(position.X + 1, position.Y + 1);

            if(isExplousionActive)
            {
                currentTickBeforeExplosion++;
                
                if (currentTickBeforeExplosion >= maxTicksBeforeExplousion)
                {
                    Explode(gameMap, Position, gameWorld.Width, gameWorld.Height, radiusOfExplousion);
                    return;
                }
            }

            if ((gameMap[downPos.X, downPos.Y].IsPassible == false &&
               gameMap[leftPos.X, leftPos.Y].IsPassible == false &&
               gameMap[rightPos.X, rightPos.Y].IsPassible == false) ||
               (gameMap[downPos.X, downPos.Y] is Player))
            {
                return;
            }
            else if (gameMap[downPos.X, downPos.Y].IsPassible == true &&
                gameMap[downPos.X, downPos.Y] is not Player &&
                currentTickBeforeFalling >= maxTicksBeforeFalling)
            {
                Fall(gameMap, position, downPos);
                currentTickBeforeFalling = 0;
            }
            else if (gameMap[downPos.X, downPos.Y].IsPassible == true &&
                gameMap[downPos.X, downPos.Y] is not Player &&
                currentTickBeforeFalling < maxTicksBeforeFalling)
            {
                currentTickBeforeFalling++;
            }
            else if ((gameMap[downPos.X, downPos.Y].IsPassible == false &&
                gameMap[downPos.X, downPos.Y] is not Player) &&
                (gameMap[leftPos.X, leftPos.Y].IsPassible == true ||
                gameMap[rightPos.X, rightPos.Y].IsPassible == true))
            {
                Roll(gameMap, position, leftPos, rightPos, newLeftDownPosition, newRightDownPosition);
            }

        }
        private void Fall(GameObject[,] gameMap, Vector2D position, Vector2D newDownPosition)
        {
            if (gameMap[newDownPosition.X, newDownPosition.Y].IsPassible)
            {
                gameMap[newDownPosition.X, newDownPosition.Y] = gameMap[position.X, position.Y];
                gameMap[newDownPosition.X, newDownPosition.Y].Position = newDownPosition;
                gameMap[position.X, position.Y] = new Air(position.X, position.Y);
            }
        }

        private void Roll(GameObject[,] gameMap, Vector2D position,
            Vector2D newLeftPosition,
            Vector2D newRightPosition,
            Vector2D newLeftDownPosition,
            Vector2D newRightDownPosition)
        {
            Random localRandom = new Random();

            if (gameMap[newLeftPosition.X, newLeftPosition.Y].IsPassible &&
                gameMap[newRightPosition.X, newRightPosition.Y].IsPassible &&
                gameMap[newLeftDownPosition.X, newLeftDownPosition.Y].IsPassible &&
                gameMap[newRightDownPosition.X, newRightDownPosition.Y].IsPassible)
            {
                if (localRandom.NextDouble() < 0.5)
                {
                    gameMap[newLeftPosition.X, newLeftPosition.Y] = gameMap[position.X, position.Y];
                    gameMap[newLeftPosition.X, newLeftPosition.Y].Position = newLeftPosition;
                    gameMap[position.X, position.Y] = new Air(position.X, position.Y);
                }
                else
                {
                    gameMap[newRightPosition.X, newRightPosition.Y] = gameMap[position.X, position.Y];
                    gameMap[newRightPosition.X, newRightPosition.Y].Position = newRightPosition;
                    gameMap[position.X, position.Y] = new Air(position.X, position.Y);
                }
            }
            else if (gameMap[newLeftPosition.X, newLeftPosition.Y].IsPassible &&
                gameMap[newLeftDownPosition.X, newLeftDownPosition.Y].IsPassible)
            {
                gameMap[newLeftPosition.X, newLeftPosition.Y] = gameMap[position.X, position.Y];
                gameMap[newLeftPosition.X, newLeftPosition.Y].Position = newLeftPosition;
                gameMap[position.X, position.Y] = new Air(position.X, position.Y);
            }
            else if (gameMap[newRightPosition.X, newRightPosition.Y].IsPassible &&
                gameMap[newRightDownPosition.X, newRightDownPosition.Y].IsPassible)
            {
                gameMap[newRightPosition.X, newRightPosition.Y] = gameMap[position.X, position.Y];
                gameMap[newRightPosition.X, newRightPosition.Y].Position = newRightPosition;
                gameMap[position.X, position.Y] = new Air(position.X, position.Y);
            }
        }

        private void Explode(GameObject[,] gameMap, Vector2D position, int width, int height, int radiusOfExplousion)
        {
            for (int i = position.X - radiusOfExplousion; i <= position.X + radiusOfExplousion; i++)
            {
                for (int j = position.Y - radiusOfExplousion; j <= position.Y + radiusOfExplousion; j++)
                {
                    if (i >= 0 && j >= 0 && i < width && j < height)
                    {
                        if (gameMap[i, j] is not Wall)
                        {
                            if (gameMap[i, j] is Player)
                            {
                                Player.IsPlayerAlive = false;
                            }

                            gameMap[i, j] = new Air(i, j);
                        }
                    }
                }
            }
        }
    }
}
