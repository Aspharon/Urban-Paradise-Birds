using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace UrbanParadiseBirds
{
    class Pigeon : GameObject
    {
        Texture2D[] northSprites, eastSprites, southSprites, westSprites, flyingSprites;
        Vector2 landingPos;
        LandingPosition landingPosition;
        Vector2 flyDirection, velocity;
        bool landed = false;
        public bool flying = true;
        int flyingCounter, spritecounter;
        Texture2D currentSprite;
        SpriteEffects spriteEffects;
        int hopPos;
        SoundEffect flap;

        public Pigeon(LandingPosition land)
        {
            northSprites = new Texture2D[3];
            eastSprites = new Texture2D[3];
            southSprites = new Texture2D[3];
            westSprites = new Texture2D[3];
            flyingSprites = new Texture2D[4];

            northSprites[0]  = Game1.contentManager.Load<Texture2D>("N1");
            northSprites[1]  = Game1.contentManager.Load<Texture2D>("N2");
            northSprites[2]  = Game1.contentManager.Load<Texture2D>("N3");
            eastSprites[0]   = Game1.contentManager.Load<Texture2D>("E1");
            eastSprites[1]   = Game1.contentManager.Load<Texture2D>("E2");
            eastSprites[2]   = Game1.contentManager.Load<Texture2D>("E3");
            southSprites[0]  = Game1.contentManager.Load<Texture2D>("S1");
            southSprites[1]  = Game1.contentManager.Load<Texture2D>("S2");
            southSprites[2]  = Game1.contentManager.Load<Texture2D>("S3");
            westSprites[0]   = Game1.contentManager.Load<Texture2D>("W1");
            westSprites[1]   = Game1.contentManager.Load<Texture2D>("W2");
            westSprites[2]   = Game1.contentManager.Load<Texture2D>("W3");
            flyingSprites[0] = Game1.contentManager.Load<Texture2D>("F1");
            flyingSprites[1] = Game1.contentManager.Load<Texture2D>("F2");
            flyingSprites[2] = Game1.contentManager.Load<Texture2D>("F3");
            flyingSprites[3] = Game1.contentManager.Load<Texture2D>("F4");

            flap = Game1.contentManager.Load<SoundEffect>("flap");

            landingPosition = land;
            landingPos = landingPosition.position;
            land.occupied = true;
            currentSprite = eastSprites[2];

            position = new Vector2(Game1.rand.Next(0, 250), -30);
            if (position.X > landingPos.X) spriteEffects = SpriteEffects.FlipHorizontally;
            else spriteEffects = SpriteEffects.None;
        }

        public override void Update(GameTime gameTime)
        {
            if (flying)
            {
                if (!landed)
                {

                    if (Math.Abs((position - landingPos).Length()) > 0.5f)
                    {
                        Fly();
                    }
                    else
                    {
                        landed = true;
                        flying = false;
                    }
                }
                else Fly();
                CycleAnimation();
            }
            else //landed
            {
                velocity.Y += 1; //gravity
                if (velocity.X == 0) //not hopping
                {
                    if (Game1.rand.Next(90) == 1) { Hop(); return; }
                    if (Game1.rand.Next(600) == 1)
                    {
                        FlyAway();
                        return;
                    }
                }
                position += velocity;
                if (position.Y > landingPos.Y) { position.Y = landingPos.Y; velocity.X = 0; velocity.Y = 1; }
            }
        }

        void FlyAway()
        {
            landingPos = new Vector2(Game1.rand.Next(0, 250), -100);
            landingPosition.occupied = false;
            flying = true;
            if (position.X > landingPos.X) spriteEffects = SpriteEffects.FlipHorizontally;
            else spriteEffects = SpriteEffects.None;
            flap.Play();
        }

        void Fly()
        {
            flyDirection = landingPos - position;
            flyDirection.Normalize();
            flyDirection *= 5;
            position += flyDirection / 8;
        }

        void CycleAnimation()
        {
            if (flying)
            {
                int animationTime = 5;
                flyingCounter++;
                if (flyingCounter == animationTime)
                {
                    flyingCounter = 0;
                    if (spritecounter == 3)
                        spritecounter = 0;
                    else
                        spritecounter++;
                    currentSprite = flyingSprites[spritecounter];
                }
            }
            else //landed
            {
                currentSprite = eastSprites[1];
            }
        }

        void Hop()
        {
            velocity.X += 1;
            velocity.Y -= 7;

            if (hopPos == 2 || (hopPos != -2 && Game1.rand.NextDouble() > 0.5)) velocity.X *= -1;

            hopPos += (int)velocity.X;

            if (velocity.X < 0) spriteEffects = SpriteEffects.FlipHorizontally;
            else spriteEffects = SpriteEffects.None;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentSprite, position, null, Color.White, 0, Vector2.Zero, 1, spriteEffects, 0);
        }
    }
}
