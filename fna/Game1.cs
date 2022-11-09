using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ExampleFNA
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D snow, background;
        public List<Snowflake> snowflakes = new List<Snowflake>();
        private KeyboardState Start = Keyboard.GetState();
        private KeyboardState Stop;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = true;
            AddSnowflakes();
            graphics.ApplyChanges();
        }

        protected void AddSnowflakes()
        {
            var rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                snowflakes.Add(new Snowflake
                {
                    X = rnd.Next(graphics.PreferredBackBufferWidth),
                    Y = -rnd.Next(graphics.PreferredBackBufferHeight),
                    Size = rnd.Next(5, 15)
                });
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("..\\..\\..\\Content/Фон.jpg");
            snow = Content.Load<Texture2D>("..\\..\\..\\Content/снежинка.png");
        }

        protected override void Update(GameTime gameTime)
        {
            Stop = Start;
            Start = Keyboard.GetState();

            foreach (var snowflake in snowflakes)
            {

                snowflake.Y += snowflake.Size/4;
                if (snowflake.Y > graphics.PreferredBackBufferHeight)
                {
                    snowflake.Y = -snowflake.Size;
                }
            }
            if (Stop.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background,
                new Rectangle(0, 0, 
                graphics.PreferredBackBufferWidth, 
                graphics.PreferredBackBufferHeight), 
                Color.White);
            foreach (var snowflake in snowflakes)
            {
                spriteBatch.Draw(snow, new Rectangle(snowflake.X,
                                                     snowflake.Y,
                                                     snowflake.Size,
                                                     snowflake.Size), Color.White);
            }
            spriteBatch.End();
        }
    }
}
