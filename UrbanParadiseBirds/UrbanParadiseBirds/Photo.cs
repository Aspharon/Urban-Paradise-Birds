using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UrbanParadiseBirds
{
    public class Photo
    {
        public Texture2D image;
        public int points;

        public Photo(Texture2D pic, int p)
        {
            image = pic;
            points = p;
        }
    }
}
