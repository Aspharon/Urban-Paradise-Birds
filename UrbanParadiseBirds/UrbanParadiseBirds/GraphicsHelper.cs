using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace UrbanParadiseBirds
{
    class GraphicsHelper
    {
        RenderTarget2D render;
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        private SpriteFont font;

        public static readonly int zoom = 4;
        public static readonly int renderWidth = 250;
        public static readonly int renderHeight = 200;

        private static readonly int windowWidth = renderWidth * zoom;
        private static readonly int windowHeight = renderHeight * zoom;

        private int currentWindowWidth = windowWidth;
        private int currentWindowHeight = windowHeight;

        Texture2D BG, FG;

        public GraphicsHelper(Game host)
        {
            graphics = new GraphicsDeviceManager(host);

            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.ApplyChanges();

            PresentationParameters pp = graphics.GraphicsDevice.PresentationParameters;
            render = new RenderTarget2D(graphics.GraphicsDevice, renderWidth, renderHeight, false, SurfaceFormat.Color, DepthFormat.None, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);

            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            BG = Game1.contentManager.Load<Texture2D>("BG");
            FG = Game1.contentManager.Load<Texture2D>("FG");

            font = Game1.contentManager.Load<SpriteFont>("File");
        }
        
        public void HandleInput(InputHelper inputHelper)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.SetRenderTarget(render);
            graphics.GraphicsDevice.Clear(Color.Transparent);
            graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            graphics.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            spriteBatch.Begin(0, null, SamplerState.PointClamp);
            spriteBatch.Draw(BG, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            foreach (GameObject obj in Objects.List)
                obj.Draw(gameTime, spriteBatch);    //No idea if this is in the correct spot
            spriteBatch.Draw(FG, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Last Photo Score: " + Game1.lastScore, Vector2.Zero, Color.White);
            spriteBatch.DrawString(font, "High Score: " + Game1.highScore, new Vector2(0, 180), Color.White);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(render, destinationRectangle: new Rectangle(0, 0, currentWindowWidth, currentWindowHeight), color: Color.White);
            spriteBatch.End();

        }
    }
}
