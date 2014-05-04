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
        private Point _mapSize = new Point(35, 30);     //the size of the map (50 columns by 50 rows)
        Color textColor = Color.White;                  //the color to use for text
        const int HUDheight = 100;                      //the height of the black band at the bottom of the screen
        int _roundsOfSmoothing = 0;                     //stores how many times the map has been smoothed over

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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _defaultFont = Content.Load<SpriteFont>("DefaultFont");
            _bigFont = Content.Load<SpriteFont>("BigFont");
            _white = Content.Load<Texture2D>("white");

            for (int i = 0; i < _heightColors.Length; i++)
            {
                int red = 0;                //no red
                int green = 200;     //green based on the height
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
            if (WasJustPressed(Keys.Escape)) { this.Exit(); }                       // Allows the game to exit
            if (WasJustPressed(Keys.F11)) { graphics.ToggleFullScreen(); }          //toggles fullscreen
            if (WasJustPressed(Keys.Space) || WasJustClicked(MouseButtons.Right))   //smooth the map
            { _map = MapGenerator.SmoothMap(_map); _roundsOfSmoothing = _roundsOfSmoothing+100; }
            if (WasJustPressed(Keys.Enter)) { CreateNewMap(); }                     //generate new map

            //CTRL + S saves the map
            if (WasJustPressed(Keys.S) && _currentKeyboard.IsKeyDown(Keys.LeftControl))
            {
                bool saveWithHeight = _currentKeyboard.IsKeyDown(Keys.LeftShift) || _currentKeyboard.IsKeyDown(Keys.RightShift);
                string newSavePath = GetSavePath();
                
                MapGenerator.SaveMapWithHeight(_map, newSavePath, (byte)_oceanHeight);
            }


            float scrollChange = (_currentMouse.ScrollWheelValue) - _oldMouse.ScrollWheelValue;
            if (scrollChange != 0)
            {
                _oceanHeight += Math.Sign(scrollChange);
                _oceanHeight = MathHelper.Clamp(_oceanHeight, 0, 255);
            }

            if (_currentMouse.LeftButton == ButtonState.Pressed && _oldMouse.LeftButton == ButtonState.Pressed)
            {
                Vector2 mouseMovement = new Vector2(_currentMouse.X, _currentMouse.Y) - new Vector2(_oldMouse.X, _oldMouse.Y);

                _mapScrollOffset += mouseMovement;
                float maxLeft = WindowSize.X - _mapSize.X * _tileSize.X;
                float maxUp = WindowSize.Y - _mapSize.Y * _tileSize.Y;
                maxLeft = Math.Min(maxLeft, 0);
                maxUp = Math.Min(maxUp, 0);

                _mapScrollOffset.X = MathHelper.Clamp(_mapScrollOffset.X, maxLeft, 0);
                _mapScrollOffset.Y = MathHelper.Clamp(_mapScrollOffset.Y, maxUp, 0);

            }
        }

        private string GetSavePath()
        {
            string folderOfMapEditor = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string newMapPath = (new DirectoryInfo(folderOfMapEditor).Parent.Parent.Parent.Parent.Parent.FullName) + "\\Advanced Tactics\\Advanced Tactics\\Map\\map2.txt";
            return newMapPath;
        }

        private void UpdateStates()
        {
            _currentKeyboard = Keyboard.GetState();
            _currentMouse = Mouse.GetState();
            _mousePosition = new Vector2(_currentMouse.X, _currentMouse.Y);
        }

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


            int visibleColumns = (int)(WindowSize.X / _white.Width)+1;
            int visibleRows = (int)(WindowSize.Y / _white.Height) +1;

            visibleColumns = (int)MathHelper.Clamp(visibleColumns, 0, _mapSize.X);
            visibleRows= (int)MathHelper.Clamp(visibleRows, 0, _mapSize.Y);

            int topVisibleRow = (int)(-_mapScrollOffset.Y / _white.Height);

            int leftVisibleColumn = (int)(-_mapScrollOffset.X / _white.Width);

            int bottomVisibleRow = topVisibleRow + visibleRows;
            int rightVisibleColumn = leftVisibleColumn + visibleColumns;

            rightVisibleColumn = (int)MathHelper.Clamp(rightVisibleColumn, 0, _mapSize.X - 1);
            bottomVisibleRow = (int)MathHelper.Clamp(bottomVisibleRow, 0, _mapSize.Y - 1);

            for (int x = leftVisibleColumn ; x <= rightVisibleColumn; x++)
            {
                for (int y = topVisibleRow; y <= bottomVisibleRow; y++)
                {
                    tileColor = new Color();
                    if (_map[x, y] >= 20) { tileColor = Color.Brown; }
                    if (_map[x, y] > 0 && _map[x, y] < 20) { tileColor = Color.Green; }
                    if (_map[x, y] == 0) { tileColor = Color.Blue; }

                    if (_map[x, y] < _oceanHeight) { tileColor = Color.Blue; }
                    position = new Vector2(x * _tileSize.X, y * _tileSize.Y) + _mapScrollOffset;

                    spriteBatch.Draw(_white, position, tileColor);      //draw the tile

                    //draw the height info on top of the tile
                    //DrawShadowString(_defaultFont, _map[x, y].ToString(), position, textColor);
                }
            }

            //draw the Heads Up Display at the bottom
            //spriteBatch.Draw(_white, new Rectangle(0, (int)(WindowSize.Y - HUDheight), (int)WindowSize.X, (int)HUDheight), Color.Black * .9f);

            ////draw info
            //Vector2 textPos = new Vector2(10, WindowSize.Y - HUDheight + 5);
            //DrawShadowString(_bigFont, "Simple map creator in XNA", textPos, Color.White);
            //textPos += Vector2.UnitY * 35;
            //DrawShadowString(_defaultFont, string.Format("Map width: {0}, height: {0}", _mapSize.X, _mapSize.Y), textPos, Color.White);
            //textPos += Vector2.UnitY * 20;
            //DrawShadowString(_defaultFont, "Ocean height: " + _oceanHeight, textPos, Color.White);
            //textPos += Vector2.UnitY * 20;
            //DrawShadowString(_defaultFont, "Times smoothed: " + _roundsOfSmoothing, textPos, Color.White);

            ////draw instructions
            //textPos = new Vector2(WindowSize.X / 2 -20, WindowSize.Y - HUDheight);
            //DrawShadowString(_defaultFont, "[ENTER] for new map", textPos, Color.LightBlue);
            //textPos += Vector2.UnitY * 20;
            //DrawShadowString(_defaultFont, "[Right mousebutton or SPACE] to smooth", textPos, Color.LightBlue);
            //textPos += Vector2.UnitY * 20;
            //DrawShadowString(_defaultFont, "[Mousescroll] to change ocean level", textPos, Color.LightBlue);
            //textPos += Vector2.UnitY * 20;
            //DrawShadowString(_defaultFont, "[CTRL+S] saves map (+ SHIFT for save with height)", textPos, Color.LightBlue);
            //textPos += Vector2.UnitY * 20;
            //DrawShadowString(_defaultFont, "Drag map with left mousebutton", textPos, Color.LightBlue);

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
