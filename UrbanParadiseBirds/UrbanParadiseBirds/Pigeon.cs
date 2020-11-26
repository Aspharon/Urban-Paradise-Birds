using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UrbanParadiseBirds
{
    class Pigeon : GameObject
    {
        Texture2D[] northSprites, eastSprites, southSprites, westSprites, flyingSprites;
        Vector2 landingPos;
        LandingPosition landingPosition;
        Vector2 flyDirection;
        bool landed = false;
        public bool flying = true;
        int flyingCounter, spritecounter;
        Texture2D currentSprite;

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

            landingPosition = land;
            landingPos = landingPosition.position;
            land.occupied = true;
            currentSprite = eastSprites[2];

            position = new Vector2(Game1.rand.Next(0, 250), -30);
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
                if (Game1.rand.Next(600) == 1)
                    FlyAway();
            }
        }

        void FlyAway()
        {
            landingPos = new Vector2(Game1.rand.Next(0, 250), -100);
            landingPosition.occupied = false;
            flying = true;
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
                if (Game1.rand.NextDouble() > 0.5)
                    currentSprite = eastSprites[1];
                else currentSprite = westSprites[1];
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentSprite, position);
        }
    }
}
