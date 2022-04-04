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

        private Widget _selected;
        private Widget _title;
        private int _borderwidth;
        private Color _selectedColor;
        private float _offsetCenter;
        private Rectangle _sampleImageRect;

        //private Texture2D _sampleImage;


        private KeyboardState previouKeyboardsState;
        private GamePadState previousGamePadState;

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <param name="font">The font for its widgets.</param>
        /// <param name="titleFont">The title font.</param>
        /// <param name="widgets">List of the widgets.</param>
        /// <param name="max_rows">The maximum amount of rows.</param>
        /// <param name="max_columns">The maximum amount of columns.</param>
        /// <param name="titleText">The title to be shown.</param>
        /// <param name="borderwidth">The borderwidth for the widgets.</param>
        /// <param name="selectedColor">Color of the selected widget.</param>
        /// <param name="offsetCenter">The horizontal(x) offset from the center. Can be negative</param>
        public Menu(SpriteFont font, SpriteFont titleFont, List<Widget> widgets = null, int max_rows = 15, int max_columns = 5, string titleText = "", int borderwidth = 4, Color selectedColor = default, float offsetCenter = 1f)
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
            _offsetCenter = offsetCenter;

            _Font = font;
            _title = AddTitle(titleText, titleFont);

        }

        /// <summary>
        /// Adds the widget.
        /// </summary>
        /// <param name="Title">The shown title.</param>
        /// <param name="stringvar">The variable for a string. (path of game)</param>
        /// <param name="img">The background image.</param>
        /// <param name="onclick">What happens when you click the Widget.</param>
        /// <param name="Font">The font of the widget.</param>
        /// <param name="size">The size of the widget.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="sampleImage">The sample image.</param>
        /// <returns></returns>
        public Widget AddWidget(String Title = "", string stringvar = null, Texture2D img = null, Action onclick = null, SpriteFont Font = null, Vector2 size = default, Texture2D icon = null, Texture2D sampleImage = null)
        {
            if (Font == null)
                Font = _Font;

            Widget widget = new Widget(Title: Title, stringvar: stringvar, img: img, onclick: onclick, Font: Font, size: size, borderwidth: _borderwidth, selectedColor: _selectedColor, icon: icon, _sampleImage: sampleImage);
            Widgets.Add(widget);
            GenerateScrolling();
            return widget;
        }

        /// <summary>
        /// Draws the menu with the specified spritebatch.
        /// </summary>
        /// <param name="sb">The sb.</param>
        public void Draw(SpriteBatch sb)
        {
            _title.Draw(sb);

            if (_selected.sampleImage != null)
                sb.Draw(_selected.sampleImage, _sampleImageRect, Color.White);

            foreach (var widget in Widgets)
            {
                widget.Draw(sb);
            }

            
        }

        /// <summary>
        /// Checks pressed keys
        /// </summary>
        /// <param name="gamepadState">Optional: State of the gamepad.</param>
        /// <param name="keyboardState">Optional: State of the keyboard.</param>
        public void Update(GamePadState gamepadState = default, KeyboardState keyboardState = default)
        {
            if (gamepadState == default)
                gamepadState = GamePad.GetState(PlayerIndex.One);
            if (keyboardState == default)
                keyboardState = Keyboard.GetState();
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
            if (capabilities.IsConnected)
            {
                // Get the current state of Controller1

                //Console.WriteLine(capabilities.GamePadType);

                // You can check explicitly if a gamepad has support for a certain feature
                if (capabilities.HasLeftXThumbStick)
                {
                    // Check teh direction in X axis of left analog stick
                    if (gamepadState.ThumbSticks.Left.X < -0.5f & !(previousGamePadState.ThumbSticks.Left.X < -0.5f))       //left
                        CtrlLeft();
                    else if (gamepadState.ThumbSticks.Left.X > 0.5f & !(previousGamePadState.ThumbSticks.Left.X > 0.5f))    //right
                        CtrlRight();
                    else if (gamepadState.ThumbSticks.Left.Y < -0.5f & !(previousGamePadState.ThumbSticks.Left.Y < -0.5f))  //down
                        CtrlDown();
                    else if (gamepadState.ThumbSticks.Left.Y > 0.5f & !(previousGamePadState.ThumbSticks.Left.Y > 0.5f))    //up
                        CtrlUp();
                }

                if (capabilities.HasAButton)
                {
                    if (gamepadState.IsButtonDown(Buttons.B) & !previousGamePadState.IsButtonDown(Buttons.B) || gamepadState.IsButtonDown(Buttons.Y) & !previousGamePadState.IsButtonDown(Buttons.Y))
                        CtrlSelect();

                    //System.Diagnostics.Debug.WriteLine(gamePadState.Buttons);
                }




                previousGamePadState = gamepadState;
                // You can also check the controllers "type"
                //if (capabilities.GamePadType == GamePadType.)
                //{
                //}
            }

            if (keyboardState.IsKeyDown(Keys.Up) & !previouKeyboardsState.IsKeyDown(Keys.Up))
                CtrlUp();
            else if (keyboardState.IsKeyDown(Keys.Down) & !previouKeyboardsState.IsKeyDown(Keys.Down))
                CtrlDown();
            else if (keyboardState.IsKeyDown(Keys.Left) & !previouKeyboardsState.IsKeyDown(Keys.Left))
                CtrlLeft();
            else if (keyboardState.IsKeyDown(Keys.Right) & !previouKeyboardsState.IsKeyDown(Keys.Right))
                CtrlRight();
            if (keyboardState.IsKeyDown(Keys.Enter) & !previouKeyboardsState.IsKeyDown(Keys.Enter))
                CtrlSelect();
            previouKeyboardsState = keyboardState;
            

        }

        /// <summary>
        /// Gets the selected widget.
        /// </summary>
        /// <returns> Returns the selected widget</returns>
        public Widget GetSelected()
        {
            return _selected;
        }

        /// <summary>
        /// Gets the widget at the specified colums/row.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public Widget GetWidgetAt(int column, int row)
        {
            if (column < 1)
                column = _columns;
            else if (column > _columns)
                column = 1;

            if (row < 1)
                row += _rows;
            else if (row > _rows)
                row -= _rows;

            int index = ((column - 1) * _rows + row) - 1;
            if (index < 0)
                index = Widgets.Count - 1;
            else if (index >= Widgets.Count)
                index = 0;
            return Widgets[index];
        }

        /// <summary>
        /// Selects the specified widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
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

        /// <summary>
        /// Sets the title.
        /// </summary>
        /// <param name="title">The title.</param>
        public void SetTitle(string title)
        {
            _title.SetTitle(title);
            GenerateScrolling();
        }

        /// <summary>
        /// Adds the title to the menu.
        /// </summary>
        /// <param name="titleText">The title text.</param>
        /// <param name="spriteFont">The sprite font.</param>
        /// <param name="img">The img.</param>
        /// <param name="size">The size.</param>
        /// <returns>widget for the title</returns>
        private Widget AddTitle(string titleText, SpriteFont spriteFont, Texture2D img = null, Vector2 size = default)
        {
            Widget title = new Widget(Title: titleText, img: img, Font: spriteFont, size: size, borderwidth: _borderwidth);
            title.SetPosition(new Vector2(Game1.windowRect.Width / 2 - title.GetRectangle().Width / 2, 50));
            //GenerateScrolling();
            return title;
        }

        // Generiere menü mit reihen uns Spalten
        private void GenerateGrid()
        {

            float useable_height = Game1.windowRect.Height - _title.GetRectangle().Bottom;
            float camera_width = Game1.windowRect.Width;
            int len = Widgets.Count;

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

        /// <summary>
        /// Generates the menu as a single row of circling widgets.
        /// </summary>
        private void GenerateScrolling()
        {
            SelectWidget(Widgets[0]);

            float top, posx, posy;              
            if (_title != null)
                top = _title.GetRectangle().Bottom;         // top of usable screen
            else
                top = 0;
            float useable_height = Game1.windowRect.Height - top;
            Vector2 center = new Vector2(Game1.windowRect.Width / 2, (useable_height / 2) + top);
            int len = Widgets.Count;

            float yIntervall = Widgets[0].GetRectangle().Height + 10;
            int HalfVisibleWidgets = Convert.ToInt32(Math.Floor(useable_height / yIntervall / 2));
            

            // Generate menu in fading single row
            for (int i = 0; i <= len - 1; i++)
            {
                int ind;
                float alpha;
                if (i <= HalfVisibleWidgets)             // from the middle Down
                {
                    ind = i;
                    posy = center.Y + (i * yIntervall) - yIntervall;
                    alpha = (float)Math.Abs(i) / HalfVisibleWidgets;
                }
                else                                    // from the middle up
                {
                    ind = len - (i - HalfVisibleWidgets);
                    posy = center.Y - ((i - HalfVisibleWidgets) * yIntervall) - yIntervall;
                    alpha = (float)Math.Abs(i - HalfVisibleWidgets) / HalfVisibleWidgets;
                }

                if (i <= HalfVisibleWidgets * 2)         // draw only visible Widgets
                {
                    Rectangle rect = Widgets[ind].GetRectangle();
                    posx = center.X * _offsetCenter - rect.Width / 2;

                    Widgets[ind].SetAlpha(1 - alpha);
                    Widgets[ind].SetPosition(new Vector2(posx, posy));
                    Widgets[ind].isEnabled = true;
                }
                else
                    Widgets[ind].isEnabled = false;
            }

            // Get the Size and Position of the Preview Image of the selected game, if it has one
            if (_selected.sampleImage != null)
            {
                float scale = useable_height / Math.Max(_selected.sampleImage.Width, _selected.sampleImage.Height); //by how much does the image need to scale down (while keeping its proportions) to fit Vertically in the Window
                if (_selected.sampleImage.Width > center.X)                                // Dont let the image go past the middle point
                    scale = center.X / _selected.sampleImage.Width  ;
                Vector2 newImgSize = new Vector2(_selected.sampleImage.Width * scale, _selected.sampleImage.Height * scale);      // final image size scaled down / up

                _sampleImageRect = new Rectangle(   Math.Max(Convert.ToInt32(center.X / _offsetCenter * 0.8 - newImgSize.X/2), 0), Convert.ToInt32(center.Y - (newImgSize.Y / 2)),
                                                    Convert.ToInt32(newImgSize.X), Convert.ToInt32(newImgSize.Y));
            }

            

        }

        /// <summary>
        /// Controls what happens when left is pressed.
        /// </summary>
        private void CtrlLeft()
        {
            //SelectWidget(GetWidgetAt(_selected.column - 1, _selected.row));
            //Game1.soundMenuMove.Play();
        }

        /// <summary>
        /// Controls what happens when right is pressed.
        /// </summary>
        private void CtrlRight()
        {
            //SelectWidget(GetWidgetAt(_selected.column + 1, _selected.row));
            //Game1.soundMenuMove.Play();
        }

        /// <summary>
        /// Controls what happens when up is pressed.
        /// </summary>
        private void CtrlUp()
        {
            var last = Widgets[Widgets.Count - 1];
            Widgets.RemoveAt(Widgets.Count - 1);
            Widgets.Insert(0, last);
            Game1.soundMenuMove.Play();
            GenerateScrolling();
            //SelectWidget(GetWidgetAt(_selected.column, _selected.row - 1)); 
        }

        /// <summary>
        /// Controls what happens when down is pressed.
        /// </summary>
        private void CtrlDown()
        {
            var first = Widgets[0];
            Widgets.RemoveAt(0);
            Widgets.Add(first);
            Game1.soundMenuMove.Play();
            GenerateScrolling();
            //SelectWidget(GetWidgetAt(_selected.column, _selected.row + 1));
        }

        /// <summary>
        /// Controls what happens when select is pressed.
        /// </summary>
        private void CtrlSelect()
        {
            Game1.soundMenuSelect.Play();
            
            _selected.onReturn.Invoke();
            
        }
    }
}
