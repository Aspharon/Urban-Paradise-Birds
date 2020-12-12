using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UrbanParadiseBirds
{
    public class UIObject : GameObject
    {
        public Texture2D sprite;

        public UIObject()
        {
            position = Vector2.Zero;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
