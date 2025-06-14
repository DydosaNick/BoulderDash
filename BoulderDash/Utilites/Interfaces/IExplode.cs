using BoulderDash.Core.GameObjects.Cells;
using BoulderDash.Core.GameObjects.Entities;
using BoulderDash.Core.GameObjects;
using BoulderDash.Core.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.Utilites.Interfaces
{
    public interface IExplode
    {
        public virtual void Explode(GameObject[,] gameMap, Vector2D position, int width, int height, int radiusOfExplousion)
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
