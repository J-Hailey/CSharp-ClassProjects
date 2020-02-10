using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotContra
{
    class ZombieLad : Enemy
    {
        public ZombieLad(int x, int y, int movementX) :
            base(1, x, y, movementX, 0)
        {
            Image = "zombie_walk";

            if (MovementX < 0)
            {
                Image = "zombie_walk_left";
            }
        }
        
        public string Image { get; private set; }

        public override string GetImage()
        {
            if (IsDead() && MovementX >= 0)
            {
                Image = "zombie_dead";
                MovementX = 0;
            }

            if (IsDead() && MovementX < 0)
            {
                Image = "zombie_dead_left";
                MovementX = 0;
            }

            return Image;
        }
    }
}
