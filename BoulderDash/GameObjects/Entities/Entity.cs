using BoulderDash.Core.Utilites;
using BoulderDash.Core.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.GameObjects.Entities
{
    public abstract class Entity : GameObject
    {
        public Entity(int x, int y) : base(x, y) { }

        public bool IsMoveable { get; set; } = true;

        public bool IsInteractionaable { get; set; } = false;

        public virtual void React(GameWorld gameWorld, Vector2D position)
        {

        }
    }
}
