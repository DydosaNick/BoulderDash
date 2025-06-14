using BoulderDash.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.GameObjects
{
    public abstract class GameObject
    {
        public GameObject() { }

        public GameObject(int x, int y)
        {
            Position = new Vector2D(x, y);
        }

        public bool IsPassible { get; set; } = false;
        public bool IsPlayerPassible { get; set; } = false;

        public char id { get; set; } = ' ';

        public Vector2D Position;
    }
}
