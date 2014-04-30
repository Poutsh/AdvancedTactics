using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection;


namespace MapGenerator
{
    public enum MouseButtons { Left, Middle, Right, }

    public class MapGen : Microsoft.Xna.Framework.Game
    {

        #region Variables and properties

        //Constante var;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState _oldMouse, _currentMouse;            //to check for mouse input
        KeyboardState _oldKeyboard, _currentKeyboard;   //to check for keyboard input
        Vector2 _mousePosition,                         //the position of the mouse cursor
            _mapScrollOffset;                           //where the map is currently scrolled to
        SpriteFont _defaultFont, _bigFont;              //fonts for debug info
        Texture2D _white;                                //textures used to draw in different colors for grass/water
        byte[,] _map;                                   //the map (stores height for each tile)
        float _oceanHeight = 10;                        //default ocean height (tiles with lower values are blue)
        private Color[] _heightColors = new Color[64];  //the different colors used to indicate height of tiles
        private Vector2 _tileSize = new Vector2(32);    //the size of the texture used (32x32 pixels)
        private Point _mapSize = new Point(50, 40);     //the size of the map (50 columns by 50 rows)
        Color textColor = Color.White;                  //the color to use for text
        const int HUDheight = 100;                      //the height of the black band at the bottom of the screen
        int _roundsOfSmoothing = 0;                     //stores how many times the map has been smoothed over

        //helpermethod to get the current size of the window
        public Vector2 WindowSize
        {
            get { return new Vector2(GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height); }
        }

        #endregion


        #region Constructor and LoadContent

        public MapGen()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;

            CreateNewMap(); //initialize the program with a new map
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load texture and fonts
            _defaultFont = Content.Load<SpriteFont>("DefaultFont");
            _bigFont = Content.Load<SpriteFont>("BigFont");
            _white = Content.Load<Texture2D>("white");

            //generate some colors from dark green to light green
            //for use as height indicators
            for (int i = 0; i < _heightColors.Length; i++)
            {
                int red = 0;                //no red
                int green = 3 * i + 32;     //green based on the height
                int blue = 32;              //a little bit of blue
                _heightColors[i] = new Color(red, green, blue);
            }
        }

        #endregion


        #region Update and related


        protected override void Update(GameTime gameTime)
        {
            UpdateStates();             //get new keyboard and mouse
            RespondToInput(gameTime);   //react to mouse and keyboard input
            base.Update(gameTime);      //call Game class' Update()
            SaveStates();               //save keyboard and mouse for comparison in next Update
        }


        private void RespondToInput(GameTime gameTime)
        {
            //****  KEYBOARD  **************************
            //react to keyboard input
            if (WasJustPressed(Keys.Escape)) { this.Exit(); }                       // Allows the game to exit
            if (WasJustPressed(Keys.F11)) { graphics.ToggleFullScreen(); }          //toggles fullscreen
            if (WasJustPressed(Keys.Space) || WasJustClicked(MouseButtons.Right))   //smooth the map
            { _map = MapGenerator.SmoothMap(_map); _roundsOfSmoothing++; }
            if (WasJustPressed(Keys.Enter)) { CreateNewMap(); }                     //generate new map

            //CTRL + S saves the map
            if (WasJustPressed(Keys.S) && _currentKeyboard.IsKeyDown(Keys.LeftControl))
            {
                bool saveWithHeight = _currentKeyboard.IsKeyDown(Keys.LeftShift) || _currentKeyboard.IsKeyDown(Keys.RightShift);
                string newSavePath = GetSavePath();
                //save the map
                MapGenerator.SaveMapWithHeight(_map, newSavePath, (byte)_oceanHeight);
                /*if (saveWithHeight) { MapGenerator.SaveMapWithHeight(_map, newSavePath, (byte)_oceanHeight); }
                else { MapGenerator.SaveMap(_map, newSavePath, (byte)_oceanHeight); }*/
                
                //open the saved file in default app
                System.Diagnostics.Process.Start(newSavePath);
            }


            //****  MOUSE  ***************************
            //check for mousescroll - to change sealevel
            float scrollChange = _currentMouse.ScrollWheelValue - _oldMouse.ScrollWheelValue;
            if (scrollChange != 0)
            {
                _oceanHeight += Math.Sign(scrollChange);
                _oceanHeight = MathHelper.Clamp(_oceanHeight, 0, 255);
            }

            //check for dragging of the map
            //if the button is down in this Update and the previous
            if (_currentMouse.LeftButton == ButtonState.Pressed && _oldMouse.LeftButton == ButtonState.Pressed)
            {
                //get how far the mouse has moved
                Vector2 mouseMovement = new Vector2(_currentMouse.X, _currentMouse.Y) - new Vector2(_oldMouse.X, _oldMouse.Y);

                //move the map by that much
                _mapScrollOffset += mouseMovement;
                float maxLeft = WindowSize.X - _mapSize.X * _tileSize.X;
                float maxUp = WindowSize.Y - _mapSize.Y * _tileSize.Y;
                //if map is bigger than the current window,
                //it is possible to scroll further to the left and top than the border
                //so we calculate how much we should be allowed to scroll beyond the window's left/top
                maxLeft = Math.Min(maxLeft, 0);
                maxUp = Math.Min(maxUp, 0);

                //ensure the scrolling doesn't go too far
                _mapScrollOffset.X = MathHelper.Clamp(_mapScrollOffset.X, maxLeft, 0);
                _mapScrollOffset.Y = MathHelper.Clamp(_mapScrollOffset.Y, maxUp, 0);

            }
        }

        //finds an unused map number and returns the full path
        private string GetSavePath()
        {
            //get the running program's folder
            string folderOfMapEditor = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            int mapNumber = 0;      //initialize the map number
            string newMapPath;      //a variable for storing the full mappath

            do
            {
                mapNumber++;        //increment the map number
                newMapPath = Path.Combine(folderOfMapEditor, string.Format("Map_[{0:000}x{1:000}]_{2:000}].txt", _mapSize.X, _mapSize.Y, mapNumber));
                
            } while (File.Exists(newMapPath));      //continue while a map with that name already exists

            return newMapPath;     //return the valid name of a non-existing map
        }


        //get current states and save mouse position
        private void UpdateStates()
        {
            _currentKeyboard = Keyboard.GetState();
            _currentMouse = Mouse.GetState();
            _mousePosition = new Vector2(_currentMouse.X, _currentMouse.Y);
        }

        //save the states for comparison in next Update()
        private void SaveStates()
        {
            _oldKeyboard = _currentKeyboard;
            _oldMouse = _currentMouse;
        }

        #endregion


        #region Draw

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            base.Draw(gameTime);

            Vector2 position;   //where to draw
            Color tileColor;    //what color to draw


            //We only want to draw the necessary tiles:
            //find out how many tiles we need to show in the map display area
            //and add an extra for when half of one is scrolled in at the right
            int visibleColumns = (int)(WindowSize.X / _white.Width)+1;
            int visibleRows = (int)(WindowSize.Y / _white.Height) +1;

            //ensure we don't show more columns than there are (for very small maps)
            visibleColumns = (int)MathHelper.Clamp(visibleColumns, 0, _mapSize.X);
            visibleRows= (int)MathHelper.Clamp(visibleRows, 0, _mapSize.Y);

            //find out which row is the topmost one we want to show
            //by dividing the current y-offset by the size of the tiles
            int topVisibleRow = (int)(-_mapScrollOffset.Y / _white.Height);

            //find out which column is the leftmost one we want to show
            //by dividing the current x-offset by the size of the tiles
            int leftVisibleColumn = (int)(-_mapScrollOffset.X / _white.Width);

            //** FIND FIRST AND LAST ROW/COLUMN TO SHOW ON SCREEN
            //add the number of visible rows to find the bottom row we need to show
            int bottomVisibleRow = topVisibleRow + visibleRows;
            //add the number of visible columns to find the rightmost column we need to show
            int rightVisibleColumn = leftVisibleColumn + visibleColumns;

            //ensure we don't show too many rows/columns
            //because of the extra tile on the left added in the visibleColumns/Rows calculation
            rightVisibleColumn = (int)MathHelper.Clamp(rightVisibleColumn, 0, _mapSize.X - 1);
            bottomVisibleRow = (int)MathHelper.Clamp(bottomVisibleRow, 0, _mapSize.Y - 1);

            //Draw all visible tiles...
            for (int x = leftVisibleColumn ; x <= rightVisibleColumn; x++)
            {
                for (int y = topVisibleRow; y <= bottomVisibleRow; y++)
                {
                    //look up the color based on the tile's height
                    tileColor = _heightColors[_map[x, y] / 4];

                    //if the ocean covers this tile, paint it blue
                    if (_map[x, y] < _oceanHeight) { tileColor = Color.Blue; }
                    //find the position based on x, y, tilesize and where the map is scrolled right now
                    position = new Vector2(x * _tileSize.X, y * _tileSize.Y) + _mapScrollOffset;

                    spriteBatch.Draw(_white, position, tileColor);      //draw the tile

                    //draw the height info on top of the tile
                    DrawShadowString(_defaultFont, _map[x, y].ToString(), position, textColor);
                }
            }

            //draw the Heads Up Display at the bottom
            spriteBatch.Draw(_white, new Rectangle(0, (int)(WindowSize.Y - HUDheight), (int)WindowSize.X, (int)HUDheight), Color.Black * .9f);

            //draw info
            Vector2 textPos = new Vector2(10, WindowSize.Y - HUDheight + 5);
            DrawShadowString(_bigFont, "MapGen", textPos, Color.White);
            textPos += Vector2.UnitY * 35;
            DrawShadowString(_defaultFont, string.Format("Map width: {0}, height: {0}", _mapSize.X, _mapSize.Y), textPos, Color.White);
            textPos += Vector2.UnitY * 20;
            DrawShadowString(_defaultFont, "Ocean height: " + _oceanHeight, textPos, Color.White);
            textPos += Vector2.UnitY * 20;
            DrawShadowString(_defaultFont, "Times smoothed: " + _roundsOfSmoothing, textPos, Color.White);

            //draw instructions
            textPos = new Vector2(WindowSize.X / 2 -20, WindowSize.Y - HUDheight);
            DrawShadowString(_defaultFont, "[ENTER] for new map", textPos, Color.LightBlue);
            textPos += Vector2.UnitY * 20;
            DrawShadowString(_defaultFont, "[Right mousebutton or SPACE] to smooth", textPos, Color.LightBlue);
            textPos += Vector2.UnitY * 20;
            DrawShadowString(_defaultFont, "[Mousescroll] to change ocean level", textPos, Color.LightBlue);
            textPos += Vector2.UnitY * 20;
            DrawShadowString(_defaultFont, "[CTRL+S] saves map (+ SHIFT for save with height)", textPos, Color.LightBlue);
            textPos += Vector2.UnitY * 20;
            DrawShadowString(_defaultFont, "Drag map with left mousebutton", textPos, Color.LightBlue);

            spriteBatch.End();

        }

        #endregion


        #region Helpermethods

        //get a new map
        private void CreateNewMap()
        {
            int smoothing = 0;
            float initalFillPercentage = .2f;
            _map = MapGenerator.CreateMap(_mapSize.X, _mapSize.Y, smoothing, initalFillPercentage);
            _roundsOfSmoothing = smoothing;
        }


        //draw a string with a shadow
        private void DrawShadowString(SpriteFont font, string text, Vector2 position, Color color)
        {
            spriteBatch.DrawString(font, text, position + Vector2.One, Color.Black);
            spriteBatch.DrawString(font, text, position, color);
        }

        //check if a key was just pressed during this Update()
        bool WasJustPressed(Keys key)
        {
            return _currentKeyboard.IsKeyDown(key) && _oldKeyboard.IsKeyUp(key);
        }

        /// <summary>
        /// Lets you know whether a mousebutton was just pressed, as opposed to having been held down for some time
        /// </summary>
        /// <param name="button">The button you want to check for</param>
        /// <returns>Whether the button was pressed since last Update</returns>
        bool WasJustClicked(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return _currentMouse.LeftButton == ButtonState.Pressed && _oldMouse.LeftButton == ButtonState.Released;

                case MouseButtons.Middle:
                    return _currentMouse.MiddleButton == ButtonState.Pressed && _oldMouse.MiddleButton == ButtonState.Released;

                case MouseButtons.Right:
                    return _currentMouse.RightButton == ButtonState.Pressed && _oldMouse.RightButton == ButtonState.Released;
            }

            return false;
        }

        #endregion

    }
}
