using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Runtime.InteropServices;

namespace GameMenuMono
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public SpriteFont Font;
        public SpriteFont titleFont;
        private Texture2D background;
        private Texture2D bttnimg;
        public static Rectangle windowRect;
        public static GraphicsDevice graphicsDevice;
        Menu.FrameCounter smartFPS = new Menu.FrameCounter(5);
        private Menu.Menu menu;
        //private Menu.EventHandler eventHandler = new Menu.EventHandler();



        public Game1()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
            //var dd = new GraphicsDevice

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.ToggleFullScreen();
            windowRect = this.Window.ClientBounds;
            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            graphicsDevice = GraphicsDevice;
            // TODO: use this.Content to load your game content here
            Font = Content.Load<SpriteFont>("UI/Font/VT323_12");
            titleFont = Content.Load<SpriteFont>("UI/Font/TitleFont");
            background = Content.Load<Texture2D>("UI/Sprites/background_01");
            bttnimg = Content.Load<Texture2D>("UI/Sprites/Button");


            menu = new Menu.Menu(Font, titleFont, max_rows: 16, titleText:"ARCADE", borderwidth: 6, selectedColor:Color.Red);
            Build();
        }

        protected override void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            menu.Update(gamePadState, keyboardState);
            smartFPS.Update(gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!IsActive)
            {
            }
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            _spriteBatch.Draw(background, new Rectangle(0, 0, windowRect.Width, windowRect.Height), Color.White);
            _spriteBatch.DrawString(Font, Math.Round(smartFPS.framerate, 4).ToString(), new Vector2(20, 20), Color.Red);
            menu.Draw(_spriteBatch);
            //if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //    _spriteBatch.DrawString(Font, "UP", new Vector2(40, 40), Color.Red);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Build()
        {
            string[] a_ext = { ".py", ".txt" };
            List<string> allowed_ext = new List<string>(a_ext);

            string gamepath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "games");
            List<string> filepaths = new List<string>();

            foreach (string folder in Directory.GetDirectories(gamepath))
            {
                string[] files = Directory.GetFiles(folder);
                foreach (string file in Directory.GetFiles(folder))
                {
                    string ext = Path.GetExtension(file);
                    if (allowed_ext.Contains(ext))
                    {
                        string bttnName = Path.GetFileName(file);
                        Action showMethod = () => runExecutable(file);
                        menu.AddWidget(Title: bttnName, stringvar: file, onclick: showMethod, Font: Font, size: new Vector2(200, 50));

                    }
                }
            }
        }
        private void runExecutable(string file)
        {

            var p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = file; 
            p.Start();
            p.WaitForExit();
            //_graphics.ToggleFullScreen();
            //_graphics.ApplyChanges();
            //System.Threading.Thread.Sleep(100);

        }
        

    }
}
