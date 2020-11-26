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
        SpriteBatch spriteBatch;

        public static readonly int zoom = 4;
        public static readonly int renderWidth = 250;
        public static readonly int renderHeight = 200;

        private static readonly int windowWidth = renderWidth * zoom;
        private static readonly int windowHeight = renderHeight * zoom;

        private int currentWindowWidth = windowWidth;
        private int currentWindowHeight = windowHeight;

        public GraphicsHelper(Game host)
        {
            graphics = new GraphicsDeviceManager(host);

            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.ApplyChanges();

            PresentationParameters pp = graphics.GraphicsDevice.PresentationParameters;
            render = new RenderTarget2D(graphics.GraphicsDevice, renderWidth, renderHeight, false, SurfaceFormat.Color, DepthFormat.None, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);

            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
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
            graphics.GraphicsDevice.Clear(Color.BlueViolet);
            graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            graphics.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            foreach (GameObject obj in Objects.List)
                obj.Draw(gameTime, spriteBatch);    //No idea if this is in the correct spot

            graphics.GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(render, destinationRectangle: new Rectangle(0, 0, currentWindowWidth, currentWindowHeight), color: Color.White);
            spriteBatch.End();

        }
    }
}
