using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameMenuMono.Menu
{
    class Widget
    {
        private string _Title;// { get; set; }
        private string _stringvar;// { get; set; }
        private Texture2D _img;// { get; set; }
        private Vector2 _position;// { get; set; }
        public Action onReturn;// { get; set; }
        public bool _is_selected;// = false;
        private Rectangle _rect;
        private Texture2D _pointTexture = new Texture2D(Game1.graphicsDevice, 1, 1);
        private SpriteFont _Font;
        public int row;
        public int column;
        private Vector2 _size;
        private Color _backgroundColor;
        private bool _centered = true;
        private Vector2 _textsize;
        private Vector2 _stringPos;
        public bool isEnabled = true;
        private float _alpha = 1f;
        private int _borderwidth;
        private Color _selectedColor;
        

        public Widget(string Title = null, string stringvar = null, Texture2D img = null, Vector2 position = default(Vector2), Action onclick = null, SpriteFont Font = null, Vector2 size = default(Vector2), Color backgroundColor = default(Color), int borderwidth = 4, Color selectedColor = default(Color))
        {
            _Title = Title;
            _stringvar = stringvar;
            _img = img;
            _borderwidth = borderwidth;
            _position = position - new Vector2(_borderwidth, _borderwidth);
            onReturn = onclick;
            _Font = Font;
            _backgroundColor = backgroundColor;
            _selectedColor = selectedColor;
            _textsize = _Font.MeasureString(_Title);
            //if (size == default(Vector2))
            //    _size = _textsize;
            //else
            
            _size = size + new Vector2(_borderwidth, _borderwidth);
            _size.X = Math.Max(_size.X, _textsize.X + _borderwidth);
            _size.Y = Math.Max(_size.Y, _textsize.Y + _borderwidth);
            
            _rect = new Rectangle(Convert.ToInt32(_position.X), Convert.ToInt32(_position.Y), Convert.ToInt32(_size.X), Convert.ToInt32(_size.Y));
            _stringPos = _position;
            

        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sb)
        {
            if (isEnabled)
            {
                if (this._is_selected)
                {
                    //Texture2D crect = new Texture2D(sb.graphicsDevice, 1, 1);
                    _pointTexture.SetData(new Color[] { Color.White });
                    //sb.Draw(_pointTexture, _rect, Color.Red);
                    DrawRectangle(sb, _rect, _selectedColor, _borderwidth);
                }
                else if (_backgroundColor != default(Color))
                {
                    _pointTexture.SetData(new Color[] { Color.White });
                    sb.Draw(_pointTexture, _rect, _backgroundColor);
                }

                sb.DrawString(_Font, _Title, _stringPos, Color.White*_alpha);
            }
        }

        public void OnClock()
        {

        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
            _rect.Offset(position - new Vector2(_rect.X, _rect.Y));
            if (_centered)
                _stringPos = _rect.Center.ToVector2() - (_textsize / 2) + new Vector2(_borderwidth/2, _borderwidth/2);
            else
                _stringPos = _position + new Vector2(_borderwidth, _borderwidth / 2);
            //_rect.Offset(position);
        }

        public Rectangle GetRectangle()
        {
            return _rect;
        }

        public void SetTitle(string title)
        {
            _Title = title;
        }

        public void SetAlpha(float alpha)
        {
            _alpha = alpha;
        }

        //static Texture2D _pointTexture;
        private void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            if (_pointTexture == null)
            {
                _pointTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _pointTexture.SetData<Color>(new Color[] { Color.White });
            }

            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + lineWidth, lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + lineWidth, lineWidth), color);
        }
    }
}
