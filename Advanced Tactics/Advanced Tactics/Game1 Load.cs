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

        protected override void Initialize()
        {
            gd = this.GraphicsDevice;

            // Gestion de la fenetre
            BufferWidth = 1280;
            BufferHeight = 720;
            Data = new Data("map2", BufferWidth, BufferHeight);

            //currentGameState = GameState.Menu;
            currentGameState = GameState.GameStart;
            message = new Message();
            ListToDraw = new List<Unit>();


            Game1.graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            gd = this.GraphicsDevice;
            spriteBatch = new SpriteBatch(GraphicsDevice);


            switch (currentGameState)
            {
                case GameState.Menu:
                    menu = new Menu(currentGameState, Data, false, Content.Load<Texture2D>("Menu/TitreJouer"), Content.Load<Texture2D>("Menu/TitreOptions"), Content.Load<Texture2D>("Menu/TitreMapEditor"), Content.Load<Texture2D>("Menu/TitreQuitter"), Content.Load<Texture2D>("Menu/OptionsReso"), Content.Load<Texture2D>("Menu/OptionsScreen"), Content.Load<Texture2D>("Menu/OptionsVolM"), Content.Load<Texture2D>("Menu/OptionsVolB"), Content.Load<Texture2D>("Menu/OptionsRetour"), Content.Load<Texture2D>("Menu/OptionsReso2"), Content.Load<Texture2D>("Menu/OptionsReso3"), Content.Load<Texture2D>("Menu/OptionsScreen2"), Content.Load<Texture2D>("Menu/OptionsVolumeB2"), Content.Load<Texture2D>("Menu/OptionsVolumeB3"), Content.Load<Texture2D>("Menu/OptionsVolumeM2"), Content.Load<Texture2D>("Menu/OptionsVolumeM3"));
                    click = Content.Load<SoundEffect>("Son/click1");
                    musicMenu = Content.Load<Song>("Son/Russian Red Army Choir");
                    MediaPlayer.Play(musicMenu);
                    sppointer = new Sprite(); sppointer.LC(Game1.Ctt, "Curseur/pointer");
                    break;


                case GameState.Option:
                    menu = new Menu(currentGameState, Data, false, Content.Load<Texture2D>("Menu/TitreJouer"), Content.Load<Texture2D>("Menu/TitreOptions"), Content.Load<Texture2D>("Menu/TitreMapEditor"), Content.Load<Texture2D>("Menu/TitreQuitter"), Content.Load<Texture2D>("Menu/OptionsReso"), Content.Load<Texture2D>("Menu/OptionsScreen"), Content.Load<Texture2D>("Menu/OptionsVolM"), Content.Load<Texture2D>("Menu/OptionsVolB"), Content.Load<Texture2D>("Menu/OptionsRetour"), Content.Load<Texture2D>("Menu/OptionsReso2"), Content.Load<Texture2D>("Menu/OptionsReso3"), Content.Load<Texture2D>("Menu/OptionsScreen2"), Content.Load<Texture2D>("Menu/OptionsVolumeB2"), Content.Load<Texture2D>("Menu/OptionsVolumeB3"), Content.Load<Texture2D>("Menu/OptionsVolumeM2"), Content.Load<Texture2D>("Menu/OptionsVolumeM3"));
                    click = Content.Load<SoundEffect>("Son/click1");
                    musicMenu = Content.Load<Song>("Son/Russian Red Army Choir");
                    MediaPlayer.Play(musicMenu);
                    sppointer = new Sprite(); sppointer.LC(Game1.Ctt, "Curseur/pointer");
                    break;

                case GameState.Loading:
                    float tempo = 5f;
                    if (gt.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                    {
                        time = gt.TotalGameTime;
                        currentGameState = GameState.GameStart;
                    }
                    break;

               case GameState.GameStart:
                    tileMap = new TileEngine(Data.fileMap, Data, map);
                    map = new Map(Data);
                    Match = new Match(Data, 2, map);


                    inGameMusic = Content.Load<SoundEffect>("Son/ingamemusic");
                    instance = inGameMusic.CreateInstance();
                    instance.IsLooped = true;
                    instance.Play();

                    sppointer = new Sprite(); sppointer.LC(Game1.Ctt, "Curseur/pointer");
                    viseur = new Viseur(Data, map.Carte, Match);

                    debug = new Debug(); debug.LoadContent();
                    Informations = new Informations(); Informations.LoadContent();

                    if (false)
                    {
                        //Unit
                        //unit = new Unit(data, "Plane", "Bishop", map.Carte, 1, 5, ListOfUnit, Match.PlayerTurn);

                        string[] arrayrang = new string[] { "AA", "Commando", "Doc", "Engineer", "Plane", "Pvt", "Tank", "Truck" };
                        string[] arrayclasse = new string[] { "Queen", "Rook", "Bishop", "Knight", "Pawn" };


                        // Fonction anonyme qui permet de faire ce que ferait une methode void sans utiliser de methode, et c'est justement l'avantage
                        // http://msdn.microsoft.com/en-us/library/dd267613(v=vs.110).aspx
                        // Cette fonction cree tous simplements plusieurs unitees
                        Func<Data, string, string, Map, int, int, Unit, Player, Match, Unit> Rdunit = (d, r, c, m, x, y, u, p, ma) => new Unit(d, r, c, m.Carte, x, y, p, ma);
                        Random rrd = new Random();
                        // Et ici j'appelle en boucle la dite fonction n fois, n etant le nombre d'unitees voulus
                        for (int i = 0; i < rrd.Next(50, 100); i++)
                            Rdunit(Data, arrayrang[rrd.Next(arrayrang.Count())], arrayclasse[rrd.Next(arrayclasse.Count())], map, rrd.Next(0, Data.MapWidth), rrd.Next(0, Data.MapHeight), unit, Match.PlayerTurn, Match);
                    }
                    break;
            }

            //// Menu
            //menu = new Menu(currentGameState, Data, false, Content.Load<Texture2D>("Menu/TitreJouer"), Content.Load<Texture2D>("Menu/TitreOptions"), Content.Load<Texture2D>("Menu/TitreMapEditor"), Content.Load<Texture2D>("Menu/TitreQuitter"), Content.Load<Texture2D>("Menu/OptionsReso"), Content.Load<Texture2D>("Menu/OptionsScreen"), Content.Load<Texture2D>("Menu/OptionsVolM"), Content.Load<Texture2D>("Menu/OptionsVolB"), Content.Load<Texture2D>("Menu/OptionsRetour"), Content.Load<Texture2D>("Menu/OptionsReso2"), Content.Load<Texture2D>("Menu/OptionsReso3"), Content.Load<Texture2D>("Menu/OptionsScreen2"), Content.Load<Texture2D>("Menu/OptionsVolumeB2"), Content.Load<Texture2D>("Menu/OptionsVolumeB3"), Content.Load<Texture2D>("Menu/OptionsVolumeM2"), Content.Load<Texture2D>("Menu/OptionsVolumeM3"));
            //click = Content.Load<SoundEffect>("Son/click1");
            //musicMenu = Content.Load<Song>("Son/Russian Red Army Choir");
            //MediaPlayer.Play(musicMenu);

            ////Game
            //inGameMusic = Content.Load<SoundEffect>("Son/ingamemusic");
            //instance = inGameMusic.CreateInstance();
            //instance.IsLooped = true;
            //instance.Play();

            //// Map

            //tileMap = new TileEngine(Data.fileMap, Data, map);
            //flou = new Sprite(); flou.LC(Game1.Ctt, "Menu/flou");


            //// Clavier, Souris
            //viseur = new Viseur(Data, map.Carte);
            //sppointer = new Sprite(); sppointer.LC(Game1.Ctt, "Curseur/pointer");

            //debug = new Debug(Data, map, viseur, ListToDraw, Match); debug.LoadContent();
            //Informations = new Informations(Data, map, viseur, ListToDraw); Informations.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
