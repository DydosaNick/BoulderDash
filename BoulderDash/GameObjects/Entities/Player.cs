using BoulderDash.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.GameObjects.Entities
{
    public class Player : Entity
    {
        public static int DiamondScore { get; set; } = 0;

        public static Vector2D CurrentPlayerPosition { get; set; } = new Vector2D(0, 0);

        public static bool IsPlayerAlive { get; set; } = true;

        public static bool IsPlayerUnderDanger { get; set; } = false;

        public Player(int x, int y) : base(x, y) 
        {
            CurrentPlayerPosition.X = x;
            CurrentPlayerPosition.Y = y;
        }
    }

}
