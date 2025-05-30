using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.Utilites;
using BoulderDash.Core.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.GameObjects.Entities
{
    public class Rock : Entity
    {
        public Rock(int x, int y) : base(x, y) 
        {  

        }
        
        private readonly int maxTicksBeforeFalling = 3;

        private int currentTickBeforeFalling = 0;

        public override void Action(GameWorld gameWorld, Vector2D position)
        {
            GameObject[,] gameMap = gameWorld.Map;

            Vector2D newLeftPosition = new Vector2D(position.X - 1, position.Y);
            Vector2D newRightPosition = new Vector2D(position.X + 1, position.Y);
            Vector2D newDownPosition = new Vector2D(position.X, position.Y + 1);
            Vector2D newLeftDownPosition = new Vector2D(position.X - 1, position.Y + 1);
            Vector2D newRightDownPosition = new Vector2D(position.X + 1, position.Y + 1);

            if (gameMap[newDownPosition.X, newDownPosition.Y].IsPassible == false &&
               gameMap[newLeftPosition.X, newLeftPosition.Y].IsPassible == false &&
               gameMap[newRightPosition.X, newRightPosition.Y].IsPassible == false)
            {
                return;
            }

            if (gameMap[newDownPosition.X, newDownPosition.Y] is Player)
            {                
                return;
            }

            if (gameMap[newDownPosition.X, newDownPosition.Y].IsPassible == true && 
                gameMap[newDownPosition.X, newDownPosition.Y] is not Player && 
                currentTickBeforeFalling >= maxTicksBeforeFalling)
            {
                Fall(gameMap, position, newDownPosition);
                currentTickBeforeFalling = 0;
            }
            else if (gameMap[newDownPosition.X, newDownPosition.Y].IsPassible == true &&
                gameMap[newDownPosition.X, newDownPosition.Y] is not Player &&
                currentTickBeforeFalling < maxTicksBeforeFalling)
            {
                currentTickBeforeFalling++;
            }
            else if ((gameMap[newDownPosition.X, newDownPosition.Y].IsPassible == false &&
                gameMap[newDownPosition.X, newDownPosition.Y] is not Player) &&
                (gameMap[newLeftPosition.X, newLeftPosition.Y].IsPassible == true ||
                gameMap[newRightPosition.X, newRightPosition.Y].IsPassible == true))
            {
                Roll(gameMap, position, newLeftPosition, newRightPosition, newLeftDownPosition, newRightDownPosition);
            }

        }
        private void Fall(GameObject[,] gameMap, Vector2D position, Vector2D newDownPosition)
        {
            if (gameMap[newDownPosition.X, newDownPosition.Y].IsPassible) 
            {
                //GameObject localFirst = gameMap[position.X, position.Y];
                //GameObject localSecond = gameMap[newDownPosition.X, newDownPosition.Y];

                gameMap[newDownPosition.X, newDownPosition.Y] = gameMap[position.X, position.Y];
                gameMap[position.X, position.Y] = new Air(position.X, position.Y);
                gameMap[newDownPosition.X, newDownPosition.Y].Position = newDownPosition;
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
    }
}

