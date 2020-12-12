using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UrbanParadiseBirds
{
    public class PhotoDisplay : GameObject
    {
        public Photo photo;

        public PhotoDisplay()
        {
            position = Vector2.Zero;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(photo.image, destinationRectangle: new Rectangle(0, 0, 250, 200), color: Color.White);
        }
    }
}
