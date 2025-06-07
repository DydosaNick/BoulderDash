using BoulderDash.Core.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulderDash.Core.Utilites
{
    public interface AbstractMetodForEntity
    {
        public void Fall(GameWorld gameWorld, int x, int y)
        {
            // Default implementation can be empty or throw an exception
            // if the entity does not support falling.
        }

        public void Roll(GameWorld gameWorld, int x, int y);

        public void Explode(GameWorld gameWorld, int x, int y);

    }
}
