using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UrbanParadiseBirds
{
    class Confetti : GameObject
    {
        Texture2D sprite;
        Vector2 moveDirection;
        Color color;
        SpriteEffects spriteEffects;
        public Confetti()
        {
            position.X = Game1.rand.Next(25, 225);
            position.Y = Game1.rand.Next(-200, -10);
            sprite = Game1.contentManager.Load<Texture2D>("confetti");
            moveDirection = Vector2.Zero;
            moveDirection.Y = Game1.rand.Next(40, 50) * 0.01f;
            moveDirection.X = Game1.rand.Next(-20, 20) * 0.01f;
            switch (Game1.rand.Next(0, 3))
            {
                case 0:
                    color = Color.Indigo;
                    break;
                case 1:
                    color = Color.Red;
                    break;
                case 2:
                    color = Color.Yellow;
                    break;
            }
            if (Game1.rand.NextDouble() > 0.5) spriteEffects = SpriteEffects.FlipHorizontally;
            else spriteEffects = SpriteEffects.None;
        }
        public override void Update(GameTime gameTime)
        {
            position += moveDirection;
            if (Game1.rand.Next(10) == 1)
            {
                if (Game1.rand.NextDouble() > 0.5) spriteEffects = SpriteEffects.FlipHorizontally;
                else spriteEffects = SpriteEffects.None;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position: position, color: color, effects: spriteEffects);
        }
    }
}
