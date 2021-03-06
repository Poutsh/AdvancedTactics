﻿using System;
using System.Collections.Generic;
using MapGenerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Advanced_Tactics
{
    public partial class Game1 : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDevice gd;
        public static ContentManager Ctt;
        public Data Data { get; set; }
        public static GraphicsDeviceManager graphics { get; set; }
        SpriteBatch spriteBatch;
        GameTime gt;

        // Clavier, Souris, Camera
        Viseur viseur;
        Sprite sppointer, flou;
        public static TimeSpan time, time2, time3, time5, time6;
        // Menu
        Menu menu;
        Song musicMenu;
        SoundEffect inGameMusic;
        SoundEffectInstance instance;
        SoundEffect click;

        public enum GameState { Menu, Option, GameMulti, GameSolo, GameStart, Exit, Loading, Winner, MapGen, ExitConfirm }
        public static GameState currentGameState;

        // Match
        Message message;

        MapGen Mapgen;
        // Map
        TileEngine tileMap;
        Map map;

        // Unit
        Match Match;
        Unit unit;
        public static bool test = true;
        List<Unit> ListToDraw;

        //Resolution
        public int BufferHeight { get { return graphics.PreferredBackBufferHeight; } set { graphics.PreferredBackBufferHeight = value; } }
        public int BufferWidth { get { return graphics.PreferredBackBufferWidth; } set { graphics.PreferredBackBufferWidth = value; } }

        Debug debug;
        Informations Info;
    }
}
