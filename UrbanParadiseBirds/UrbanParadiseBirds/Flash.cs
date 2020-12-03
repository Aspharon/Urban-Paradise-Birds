using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UrbanParadiseBirds
{
    class Flash : GameObject
    {
        Texture2D sprite;
        public float opacity = 0;

        public Flash() //For anyone reading this: Don't do this. This is a very bodged approach to doing this, because I implemented the foreground/background badly. 
        {
            sprite = Game1.contentManager.Load<Texture2D>("Flash");
        }

        public override void Update(GameTime gameTime)
        {
            opacity -= 0.02f;
            if (opacity < 0) opacity = 0;
        }

        public void DrawFlash(GameTime gameTime, SpriteBatch spriteBatch) //I hate this.
        {
            spriteBatch.Draw(sprite, position, null, Color.White * opacity, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
