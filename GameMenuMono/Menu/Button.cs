using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameMenuMono.Menu
{
    class Button
    {
        public string bttnName { get; set; }
        public string gamePath { get; set; }
        //public Rectangle rect { get; set; }
        public Texture2D img { get; set; }

        public Vector2 position { get; set; }

        public Action onclick { get; set; }

        public bool is_selected = false;

        public SpriteFont BttnFont { get; set; }

        private Game1 game = new Game1();

        static void init()
        {
            
            //string bttnName = name;
            //string gamePath = path;
            //Texture2D _img = img;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sb, GraphicsDevice graphics)
        {
            Vector2 size = BttnFont.MeasureString(bttnName);
            
            
            if (this.is_selected)
            {

                Texture2D rect = new Texture2D(graphics, 1, 1);//System.Convert.ToInt32(Math.Round(size.X)), System.Convert.ToInt32(Math.Round(size.Y)));
                 rect.SetData(new Color[] { Color.White });
                
                 //sb.Draw(rect, new Rectangle(Convert.ToInt32(Math.Round(size.X)), Convert.ToInt32(Math.Round(size.Y)));
                sb.Draw(rect, new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), Convert.ToInt32(size.X), Convert.ToInt32(size.Y)),Color.Red);
                //sb.Draw(rect, new Vector2(10f, 20f), null,Color.Chocolate, 0f, Vector2.Zero, new Vector2(80f, 30f), SpriteEffects.None, 0f);
            }
            sb.DrawString(BttnFont, bttnName, position, Color.White);
        }

        public void OnClock()
        {

        }
    }
}
