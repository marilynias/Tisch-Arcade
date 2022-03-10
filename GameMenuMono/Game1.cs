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
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.ToggleFullScreen();
            windowRect = this.Window.ClientBounds;
            this.Window.Title = "Arcade Menu";
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
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Build()
        {
            string[] a_ext = { ".py", ".txt" };
            List<string> allowed_ext = new List<string>(a_ext);
            string gamepath;

            // Spiele ausführbar machen
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                gamepath = Path.Combine(Path.DirectorySeparatorChar.ToString(), "home", "arcade", "Arcade", "games");
                var p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = "bash";
                p.StartInfo.Arguments = "-c 'for file in /home/arcade/Arcade/games/*/*.* ; do dos2unix \"$file\" \"$file\"; chmod +x \"$file\"; done'";
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();
                //while (!p.StandardOutput.EndOfStream)
                //{
                //    string line = p.StandardOutput.ReadLine();
                //    Console.WriteLine(line);
                //}
            }
            else
                gamepath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "games");
            List<string> filepaths = new List<string>();

            foreach (string folder in Directory.GetDirectories(gamepath))
            {
                string[] files = Directory.GetFiles(folder);
                foreach (string file in Directory.GetFiles(folder))
                {
                    string ext = Path.GetExtension(file);
                    if (allowed_ext.Contains(ext))
                    {
                        string bttnName = Path.GetFileNameWithoutExtension(file);
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

            // Fenster wieder in den Vordergrund holen
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                var ps = new Process();
                ps.StartInfo.UseShellExecute = false;
                ps.StartInfo.FileName = "bash";
                ps.StartInfo.Arguments = "-c 'xdotool search \"Arcade Menu\" windowactivate --sync key --clearmodifiers ctrl+l'";
                ps.StartInfo.RedirectStandardOutput = true;
                ps.Start();
                while (!ps.StandardOutput.EndOfStream)
                {
                    string line = ps.StandardOutput.ReadLine();
                    Console.WriteLine(line);
                }
            }


        }


    }
}
