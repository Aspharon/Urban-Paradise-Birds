using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UrbanParadiseBirds
{
    public class PhotoIcon : GameObject
    {
        public bool taken;
        Texture2D blankSprite, takenSprite, sprite;

        public PhotoIcon(Vector2 pos)
        {
            blankSprite = Game1.contentManager.Load<Texture2D>("photoBlank");
            takenSprite = Game1.contentManager.Load<Texture2D>("photoTaken");
            sprite = blankSprite;
            position = pos;
        }

        public override void Update(GameTime gameTime)
        {
            if (taken) sprite = takenSprite;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}
