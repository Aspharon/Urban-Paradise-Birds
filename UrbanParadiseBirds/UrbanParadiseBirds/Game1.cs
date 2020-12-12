using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace UrbanParadiseBirds
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private InputHelper inputHelper;
        private GraphicsHelper graphicsHelper;
        private LandingPosition[] lpos;
        private PhotoIcon[] icons;
        public static ContentManager contentManager;
        public static Random rand;
        public static int lastScore;
        public static int highScore;
        List<Photo> photos;
        PhotoDisplay display;
        Song ambience;
        SoundEffect shutter, partyhorn;
        
        int maxPhotos = 5, photosTaken, timer, selectedPhoto = 0;
        

        public Game1()
        {
            contentManager = Content;
            contentManager.RootDirectory = "Content";
            graphicsHelper = new GraphicsHelper(this);
            inputHelper = new InputHelper();
            IsMouseVisible = true;
            rand = new Random();
            photos = new List<Photo>();
            icons = new PhotoIcon[maxPhotos];
        }

        protected override void Initialize()
        {
            lpos = new LandingPosition[5];
            lpos[0] = new LandingPosition(new Vector2(50, 132));
            Objects.List.Add(lpos[0]);

            lpos[1] = new LandingPosition(new Vector2(70, 62));
            Objects.List.Add(lpos[1]);

            lpos[2] = new LandingPosition(new Vector2(135, 45));
            Objects.List.Add(lpos[2]);

            lpos[3] = new LandingPosition(new Vector2(150, 11));
            Objects.List.Add(lpos[3]);

            lpos[4] = new LandingPosition(new Vector2(170, 132));
            Objects.List.Add(lpos[4]);

            SpawnPigeon();

            ambience = contentManager.Load<Song>("ambience");
            shutter = contentManager.Load<SoundEffect>("shutter");
            partyhorn = contentManager.Load<SoundEffect>("partyhorn");
            MediaPlayer.Play(ambience);
            MediaPlayer.IsRepeating = true;

            for (int z = 0; z < maxPhotos; z++)
            {
                icons[z] = new PhotoIcon(new Vector2(z * 22 + 2, 200-27));
                Objects.List.Add(icons[z]);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {

        }

        void SpawnPigeon()
        {
            int index = rand.Next(lpos.Length);
            LandingPosition landing = lpos[index];
            if (!landing.occupied)
            {
                Pigeon pigeon = new Pigeon(landing);
                Objects.List.Add(pigeon);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
            graphicsHelper.Update(gameTime);
            graphicsHelper.HandleInput(inputHelper);
            foreach (GameObject obj in Objects.List)
                obj.HandleInput(inputHelper);
            foreach (GameObject obj in Objects.List)
                obj.Update(gameTime);
            foreach (GameObject obj in Objects.AddList)
                Objects.List.Add(obj);
            Objects.AddList.Clear();
            foreach (GameObject obj in Objects.RemoveList)
                Objects.List.Remove(obj);
            Objects.RemoveList.Clear();

            switch (graphicsHelper.gamestate) {
                case 0:
                {
                    if (rand.Next(600) == 1) //spawn pigeon
                    {
                        SpawnPigeon();
                    }
                    if (timer > 0)
                    {
                        timer++;
                        MediaPlayer.Volume -= 0.01f;
                        if (MediaPlayer.Volume < 0) { MediaPlayer.Volume = 0; MediaPlayer.Stop(); }
                    }
                    if (timer > 120)
                    {

                        graphicsHelper.gamestate = 1;
                        Objects.List.Clear();

                        display = new PhotoDisplay();
                        display.photo = photos[0];
                        Objects.List.Add(display);

                        UIObject frame = new UIObject();
                        frame.sprite = contentManager.Load<Texture2D>("frame");
                        Objects.List.Add(frame);
                    }


                    List<Pigeon> removeList = new List<Pigeon>();
                    foreach (Pigeon pigeon in Objects.List.OfType<Pigeon>())
                        if (pigeon.position.Y < -40)
                            removeList.Add(pigeon);
                    foreach (Pigeon p in removeList)
                    {
                        Objects.List.Remove(p);
                    }

                    if (inputHelper.KeyPressed(Keys.Space))
                    {
                        if (photosTaken < maxPhotos)
                        {
                            shutter.Play();
                            lastScore = 0;
                            foreach (Pigeon pigeon in Objects.List.OfType<Pigeon>())
                            {
                                if (pigeon.flying)
                                    lastScore += 15;
                                else
                                    lastScore += 10;
                            }
                            if (lastScore > highScore)
                                highScore = lastScore;
                            graphicsHelper.flash.opacity = 1f; //I cry myself to sleep, please implement this differently 
                            photos.Add(new Photo(graphicsHelper.Photo(gameTime), lastScore));
                            icons[photosTaken].taken = true;
                            photosTaken++;
                            if (photosTaken == maxPhotos) timer = 1;
                        }
                    }
                    break;
                }
                case 1:
                {
                    if (inputHelper.KeyPressed(Keys.Right))
                        {
                            selectedPhoto++;
                            if (selectedPhoto >= maxPhotos) selectedPhoto = 0;
                            display.photo = photos[selectedPhoto];
                        }
                    if (inputHelper.KeyPressed(Keys.Left))
                        {
                            selectedPhoto--;
                            if (selectedPhoto < 0) selectedPhoto = maxPhotos - 1;
                            display.photo = photos[selectedPhoto];
                        }
                        if (inputHelper.KeyPressed(Keys.Space))
                        {
                            graphicsHelper.gamestate++;
                            highScore = photos[selectedPhoto].points;
                            Objects.List.Clear();

                            display = new PhotoDisplay();
                            display.photo = photos[selectedPhoto];
                            Objects.List.Add(display);

                            partyhorn.Play();

                            for (int z = 0; z < 30; z++)
                            {
                                Confetti confetti = new Confetti();
                                Objects.List.Add(confetti);
                            }

                            UIObject frame = new UIObject();
                            frame.sprite = contentManager.Load<Texture2D>("endFrame");
                            Objects.List.Add(frame);
                        }
                        break;
                }
            }
        }
        
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.SetRenderTarget(null);
            graphicsHelper.Draw(gameTime);
        }
    }
}
