using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UrbanParadiseBirds
{
    class LandingPosition : GameObject
    {
        public bool occupied = false;

        public LandingPosition(Vector2 land)
        {
            position = land;
        }
    }
}
