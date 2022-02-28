using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameMenuMono
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont BttnFont;
        private string BttnText;
        private Texture2D background;
        private Texture2D bttnimg;
        private List<Menu.Button> bttnlist;
        public Rectangle windowRect;
        Menu.FrameCounter smartFPS = new Menu.FrameCounter(5);




        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            bttnlist = Menu.BuildMenu.Build(bttnimg, windowRect);
            windowRect = this.Window.ClientBounds;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            BttnFont = Content.Load<SpriteFont>("UI/Font/BttnFont");
            background = Content.Load<Texture2D>("UI/Sprites/Background_01");
            BttnText = "Game Name";
            bttnimg = Content.Load<Texture2D>("UI/Sprites/Button");


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            // TODO: Add your update logic here


            smartFPS.Update(gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            _spriteBatch.DrawString(BttnFont, BttnText, new Vector2(100, 100), Color.White);
            var fps = smartFPS.framerate;
            int height = 100;
            foreach (Menu.Button bttn in bttnlist)
            {
                _spriteBatch.DrawString(BttnFont, bttn.bttnName, new Vector2(400, height), Color.White);
                height += 15;
            }
            


            _spriteBatch.End();

            base.Draw(gameTime);
        }


        
    }
}
