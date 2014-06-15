using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Advanced_Tactics
{
    public partial class Game1 : Microsoft.Xna.Framework.Game
    {
        int once2 = 0;
        protected override void Update(GameTime gameTime)
        {
            // Init entrees utilisateur
            Inputs.Update();
            sppointer.Update(gameTime);
            //menu.InGame = true;

            float temp4 = 3f;
            switch (currentGameState)
            {
                case GameState.Menu:
                    menu.Update(gameTime);
                    if (Inputs.Clickg()) click.Play();
                    break;

                case GameState.Option:
                    menu.Update(gameTime);
                    if (menu.changeres == true && BufferWidth != (int)Data.WindowWidth)
                    {
                        BufferWidth = (int)Data.WindowWidth;
                        BufferHeight = (int)Data.WindowHeight;
                        Game1.graphics.ApplyChanges();
                        menu.changeres = false;
                    }
                    if (menu.changeres2 == true && graphics.IsFullScreen != menu.Fullscreen)
                    {
                        graphics.IsFullScreen = menu.Fullscreen;
                        Game1.graphics.ApplyChanges();
                        menu.changeres2 = false;
                    }
                    break;


                case GameState.Loading:
                    float tempo = 5.1f;
                    float tempo2 = 1f;
                    message.Update(gameTime);

                    if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                    {
                        time = gameTime.TotalGameTime;
                        currentGameState = GameState.GameStart;
                    }
                    else
                    {
                        if (gameTime.TotalGameTime - time2 > TimeSpan.FromSeconds(tempo2))
                        {
                            time2 = gameTime.TotalGameTime;
                            if (once2 == 0) message.Messages.Add(new DisplayMessage("Loading...", TimeSpan.FromSeconds(1), new Vector2(gd.Viewport.Width / 2 - message.font.MeasureString("Loading...").X / 2, gd.Viewport.Height / 2 - message.font.MeasureString("Loading...").Y / 2), Color.White));
                            if (once2 % 2 == 0 && once2 < 3)
                                message.Messages.Add(new DisplayMessage("Loading...", TimeSpan.FromSeconds(1), new Vector2(Data.WindowWidth / 2 - message.font.MeasureString("Loading...").X / 2, gd.Viewport.Height / 2 - message.font.MeasureString("Loading...").Y / 2), Color.White));
                            once2++;
                        }
                    }
                    break;

                case GameState.Winner:
                    message.Update(gameTime);
                    message.Messages.Add(new DisplayMessage(Match.Winner.PlayerName + " WIN", TimeSpan.FromSeconds(0.9), new Vector2(gd.Viewport.Width / 2 - message.font.MeasureString("Player 1  WIN").X / 2, gd.Viewport.Height / 2 - message.font.MeasureString("Player 1  WIN").Y / 2), Match.Winner.ColorSide));
                    message.Messages.Add(new DisplayMessage("enter key for retry", TimeSpan.FromSeconds(0.9), new Vector2(gd.Viewport.Width / 2 - message.font.MeasureString("enter key for retry").X / 2, gd.Viewport.Height / 2 + message.font.MeasureString("Player 1  WIN").Y + 10 - message.font.MeasureString("enter key for retry").Y / 2), Match.Winner.ColorSide));
                    if (Inputs.Keyr(Keys.Enter))
                    {
                        UnloadContent();
                        menu.MenuPrincipal = true;
                        menu.Options = false;
                        menu.InGame = false;
                        currentGameState = GameState.Menu;
                    }
                    break;

                case GameState.Game:
                    message.Update(gameTime);
                    if (Match.Winner != null)
                    {
                        currentGameState = GameState.Winner;
                    }
                    else
                    {
                        debug.Update(Data, map, viseur, ListToDraw, Match);
                        MediaPlayer.Stop();

                        Match.Update(gameTime, spriteBatch, viseur, ListToDraw);

                        viseur.Update(gameTime, ListToDraw, spriteBatch);

                        Informations.Update(Data, map.Carte);

                        instance.Volume = 0.4f;

                        if (Inputs.Keyr(Keys.Escape)) goto case GameState.Exit;
                    }
                    break;


                case GameState.GameStart:
                    LoadContent();
                    debug.Update(Data, map, viseur, ListToDraw, Match);
                    Match.Update(gameTime, spriteBatch, viseur, ListToDraw);
                    currentGameState = GameState.Game;
                    break;


                case GameState.Exit:
                    UnloadContent();
                    Exit();
                    base.Update(gameTime);
                    return;
            }
            base.Update(gameTime);
        }

        int once = 1;
        bool checkExitKey()
        {
            if (Inputs.Keyr(Keys.Escape))
            {
                Exit();
                if (once == 1)
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.FileName = "MapGenerator";
                    process.StartInfo = startInfo;
                    process.Start();
                    once--;
                }

                return true;
            }
            return false;
        }
    }
}
