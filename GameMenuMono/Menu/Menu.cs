using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GameMenuMono.Menu
{
    class Menu
    {
        private int _Max_rows;
        private int _Max_columns;

        private int _columns;
        private int _rows;

        public List<Widget> Widgets;
        private SpriteFont _Font;
        //private SpriteFont _titleFont;
        //private string _titleText;

        private Widget _selected;
        private Widget _title;
        private int _borderwidth;
        private Color _selectedColor;

        private KeyboardState previouKeyboardsState;
        private GamePadState previousGamePadState;

        public Menu(SpriteFont font, SpriteFont titleFont , Texture2D img = null, List<Widget> widgets = null, int max_rows = 15, int max_columns = 5, string titleText = "", int borderwidth= 4, Color selectedColor = default(Color))
        {
            SpriteFont Font = font;
            if (widgets == null)
                Widgets = new List<Widget>();
            else
                Widgets = widgets;
            _Max_rows = max_rows;
            _Max_columns = max_columns;
            _borderwidth = borderwidth;
            _selectedColor = selectedColor;

            _Font = font;
            //_titleFont = titleFont;
            //_titleText = title;
            _title = AddTitle(titleText, titleFont);
            
        }
        
        public Widget AddWidget(String Title = "", string stringvar = null, Texture2D img = null, Action onclick = null, SpriteFont Font = null, Vector2 size = default(Vector2))
        {
            if (Font == null)
                Font = _Font;

            Widget widget = new Widget(Title: Title, stringvar: stringvar, img: img, onclick: onclick, Font: Font, size: size, borderwidth: _borderwidth, selectedColor: _selectedColor);
            Widgets.Add(widget);
            //GenerateScrolling();
            return widget;
        }

        public void Draw(SpriteBatch sb)
        {
            _title.Draw(sb);

            foreach (var widget in Widgets)
            {
                widget.Draw(sb);
                
            }
        }
        
        public void Update(GamePadState gamePadState, KeyboardState keyboardstate)
        {
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
            if (capabilities.IsConnected)
            {
                // Get the current state of Controller1

                Console.WriteLine(capabilities.GamePadType);

                // You can check explicitly if a gamepad has support for a certain feature
                if (capabilities.HasLeftXThumbStick)
                {
                    // Check teh direction in X axis of left analog stick
                    if (gamePadState.ThumbSticks.Left.X < -0.5f & !(previousGamePadState.ThumbSticks.Left.X < -0.5f))       //left
                        CtrlLeft();
                    else if (gamePadState.ThumbSticks.Left.X > 0.5f & !(previousGamePadState.ThumbSticks.Left.X > 0.5f))    //right
                        CtrlRight();
                    else if (gamePadState.ThumbSticks.Left.Y < -0.5f & !(previousGamePadState.ThumbSticks.Left.Y < -0.5f))  //down
                        CtrlDown();
                    else if (gamePadState.ThumbSticks.Left.Y > 0.5f & !(previousGamePadState.ThumbSticks.Left.Y > 0.5f))    //up
                        CtrlUp();
                }

                if (capabilities.HasAButton)
                {
                    if (gamePadState.IsButtonDown(Buttons.X) & !previousGamePadState.IsButtonDown(Buttons.X))
                        _selected.onReturn.Invoke();
                }


                

                previousGamePadState = gamePadState;
                // You can also check the controllers "type"
                //if (capabilities.GamePadType == GamePadType.)
                //{
                //}
            }

            if (keyboardstate.IsKeyDown(Keys.Up) & !previouKeyboardsState.IsKeyDown(Keys.Up))
                CtrlUp();
            else if (keyboardstate.IsKeyDown(Keys.Down) & !previouKeyboardsState.IsKeyDown(Keys.Down))
                CtrlDown();
            else if (keyboardstate.IsKeyDown(Keys.Left) & !previouKeyboardsState.IsKeyDown(Keys.Left))
                CtrlLeft();
            else if (keyboardstate.IsKeyDown(Keys.Right) & !previouKeyboardsState.IsKeyDown(Keys.Right))
                CtrlRight();
            if (keyboardstate.IsKeyDown(Keys.Enter) & !previouKeyboardsState.IsKeyDown(Keys.Enter))
                _selected.onReturn.Invoke();
            previouKeyboardsState = keyboardstate;

            GenerateScrolling();
        }
        
        public Widget GetSelected()
        {
            return _selected;
        }

        public Widget GetWidgetAt(int x, int y)
        {
            if (x < 1)
                x = _columns;
            else if (x > _columns)
                x = 1;

            if (y < 1)
                y += _rows;
            else if (y > _rows)
                y -= _rows;

            int index = ((x-1) * _rows + y)-1;
            if (index < 0)
                index = Widgets.Count -1;
            else if (index >= Widgets.Count)
                index = 0;
            return Widgets[index];
        }

        public void SelectWidget(Widget widget)
        {
            //Widgets.Find(i => i._is_selected == true);

            //foreach (Widget widget1 in Widgets)
            //{
            //    if (widget1._is_selected)
            //        widget1._is_selected = false;
            //}
            if (_selected != null)
            {
                _selected._is_selected = false;
            }            
            _selected = Widgets[Widgets.IndexOf(widget)];
            _selected._is_selected = true;
        }

        public void SetTitle(string title)
        {
            _title.SetTitle(title);
            GenerateScrolling();
        }

        private Widget AddTitle(string titleText, SpriteFont spriteFont, Texture2D img = null, Vector2 size = default(Vector2))
        {
            Widget title = new Widget(Title: titleText, img: img, Font: spriteFont, size: size, borderwidth: _borderwidth);
            title.SetPosition(new Vector2(Game1.windowRect.Width / 2 - title.GetRectangle().Width / 2, 50));
            return title;
        }

        // Generiere menü mit riehen uns Spalten
        private void UpdateWidgets()
        {

            float useable_height = Game1.windowRect.Height - _title.GetRectangle().Bottom;
            float camera_width = Game1.windowRect.Width;
            int len = Widgets.Count;

            //else
            //    _columns = len / _rows;
            if (len < _Max_rows)
                _rows = len;
            else
                _rows = _Max_rows;

            _columns = (len / _Max_rows) + 1;
            if (_columns > _rows)
                _columns = _Max_columns;

            float x_intervall = camera_width / (_columns + 1);
            float y_intervall = (useable_height / (_Max_rows + 1));
            float posx, posy;
            int cur_col, cur_row;
            SelectWidget(Widgets[0]);

            int i = 0;
            foreach (var widget in Widgets.ToArray())
            {
                Rectangle rect = widget.GetRectangle();
                cur_col = (i / _Max_rows + 1);
                cur_row = i % _Max_rows + 1;
                if (cur_col > _Max_columns || cur_row > _Max_rows)
                    Widgets.Remove(widget);
                // make sure its Ordered top to bottom left to right
                posx = (x_intervall * cur_col) - (rect.Width / 2);
                posy = (cur_row * y_intervall) - (rect.Height / 2) + Game1.windowRect.Height - useable_height;
                widget.SetPosition(new Vector2(posx, posy));
                widget.column = cur_col;
                widget.row = cur_row;
                i++;
            }

            //int i = 0;
        }

        private void GenerateScrolling()
        {
            float useable_height = Game1.windowRect.Height - _title.GetRectangle().Bottom;
            float useable_width = Game1.windowRect.Width;
            int len = Widgets.Count;
            float yIntervall = Widgets[0].GetRectangle().Height + 10;
            int numVisibleWidgets = Convert.ToInt32(Math.Floor(useable_height / yIntervall / 2));
            //Console.WriteLine(numVisibleWidgets);
            //if (numVisibleWidgets*2 > Widgets.Count)
            SelectWidget(Widgets[0]);
            float posx, posy;
            
            float middle = (useable_height / 2) + (Game1.windowRect.Height - useable_height) - yIntervall;

            for (int i = 0; i <= Widgets.Count-1; i++)
            {
                int ind;
                float alpha;
                if (i <= numVisibleWidgets)
                {
                    ind = i;
                    posy = middle + (i * yIntervall);
                    alpha = (float)Math.Abs(i) / numVisibleWidgets;
                }
                else
                {
                    ind = Widgets.Count - (i - numVisibleWidgets);
                    posy = middle - ((i - numVisibleWidgets) * yIntervall);
                    alpha = (float)Math.Abs(i-numVisibleWidgets) / numVisibleWidgets;
                }
   
                if (i <= numVisibleWidgets * 2)
                {
                    Rectangle rect = Widgets[ind].GetRectangle();
                    posx = useable_width / 2 - rect.Width / 2;
                    
                    Widgets[ind].SetAlpha(1 - alpha);
                    Widgets[ind].SetPosition(new Vector2(posx, posy));
                    Widgets[ind].isEnabled = true;
                }
                else
                    Widgets[ind].isEnabled = false;
            }
            //int i = 1;
            //foreach (var widget in Widgets.ToArray())
            //{
            //    Rectangle rect = widget.GetRectangle();
            //    posx = useable_width / 2 - rect.Width / 2;
            //    if (i < numVisibleWidgets/2)
            //    { 
            //        posy = middle + (i * yIntervall);                    
            //    }
            //    else
            //        posy = middle - ((i - numVisibleWidgets/2) * yIntervall);
            //    if (i < numVisibleWidgets)
            //    {
            //        float alpha = (float)i / numVisibleWidgets;
            //        widget.SetAlpha(1-alpha);
            //        widget.SetPosition(new Vector2(posx, posy));
            //        widget.isEnabled = true;
            //    }
            //    else
            //        widget.isEnabled = false;
            //    i += 1;
            //}
        }

        private void CtrlLeft()
        {
            //SelectWidget(GetWidgetAt(_selected.column - 1, _selected.row));
        }
            
        private void CtrlRight()
        {
            //SelectWidget(GetWidgetAt(_selected.column + 1, _selected.row));
        }

        private void CtrlUp()
        {
            var last = Widgets[Widgets.Count - 1];
            Widgets.RemoveAt(Widgets.Count - 1);
            Widgets.Insert(0, last);
            //SelectWidget(GetWidgetAt(_selected.column, _selected.row - 1)); 
        }
        
        private void CtrlDown()
        {
            var first = Widgets[0];
            Widgets.RemoveAt(0);
            Widgets.Add(first);
            //SelectWidget(GetWidgetAt(_selected.column, _selected.row + 1));
        }
            
    }
}
