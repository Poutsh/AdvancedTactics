using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Net;


namespace Advanced_Tactics
{
    public partial class Game1 : Microsoft.Xna.Framework.Game
    {
        #region CONSTRUCTEUR

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Ctt = Content;
            graphics.IsFullScreen = false;
            this.Window.Title = "Advanced Tactics";

            // Gestion souris
            IsMouseVisible = false;

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 1);

            Game1.graphics.ApplyChanges();
        }

        #endregion

        // // // // // // // // 

        #region INIT + LOAD + UNLOAD

        protected override void Initialize()
        {
            gd = this.GraphicsDevice;

            // Gestion de la fenetre
            BufferWidth = 1280;
            BufferHeight = 720;
            Data = new Data("map2", BufferWidth, BufferHeight);
            ListToDraw = new List<Unit>();
            ListMenu = new List<Texture2D>();

            Game1.graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            gd = this.GraphicsDevice;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Menu
            menu = new Menu(Data, false, Content.Load<Texture2D>("Menu/TitreJouer"), Content.Load<Texture2D>("Menu/TitreOptions"), Content.Load<Texture2D>("Menu/TitreMapEditor"), Content.Load<Texture2D>("Menu/TitreQuitter"), Content.Load<Texture2D>("Menu/OptionsReso"), Content.Load<Texture2D>("Menu/OptionsScreen"), Content.Load<Texture2D>("Menu/OptionsVolM"), Content.Load<Texture2D>("Menu/OptionsVolB"), Content.Load<Texture2D>("Menu/OptionsRetour"), Content.Load<Texture2D>("Menu/OptionsReso2"), Content.Load<Texture2D>("Menu/OptionsReso3"), Content.Load<Texture2D>("Menu/OptionsScreen2"), Content.Load<Texture2D>("Menu/OptionsVolumeB2"), Content.Load<Texture2D>("Menu/OptionsVolumeB3"), Content.Load<Texture2D>("Menu/OptionsVolumeM2"), Content.Load<Texture2D>("Menu/OptionsVolumeM3"));
            click = Content.Load<SoundEffect>("Son/click1");
            musicMenu = Content.Load<Song>("Son/Russian Red Army Choir");
            MediaPlayer.Play(musicMenu);

            //Game
            inGameMusic = Content.Load<SoundEffect>("Son/ingamemusic");
            instance = inGameMusic.CreateInstance();
            instance.IsLooped = true;
            instance.Play();


            // Map
            map = new Map(Data);
            tileMap = new TileEngine(Data.fileMap, Data, map);
            flou = new Sprite(); flou.LC(Game1.Ctt, "Menu/flou");


            // Clavier, Souris
            viseur = new Viseur(Data, map.Carte);
            sppointer = new Sprite(); sppointer.LC(Game1.Ctt, "Curseur/pointer");

            // Unit


        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        #endregion
    }
}
