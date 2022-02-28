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


        static void init()
        {
            //string bttnName = name;
            //string gamePath = path;
            //Texture2D _img = img;
        }

        public void Update()
        {

        }

        public void OnClock()
        {

        }
    }
}
