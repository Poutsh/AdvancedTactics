using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Advanced_Tactics
{
    public partial class Game1 : Microsoft.Xna.Framework.Game
    {
        int once2 = 1;
        protected override void Update(GameTime gameTime)
        {
            // Init entrees utilisateur
            Inputs.Update();
            sppointer.Update(gameTime);
            //menu.InGame = true;

            switch (currentGameState)
            {
                case GameState.Menu:
                    menu.Update(gameTime);
                    if (Inputs.Clickg()) click.Play();
                    if (!menu.InGame && !menu.MenuPrincipal && menu.Options)
                    {
                        graphics.PreferredBackBufferWidth = (int)Data.WindowWidth;
                        graphics.PreferredBackBufferHeight = (int)Data.WindowHeight;
                        graphics.IsFullScreen = menu.Fullscreen;
                        Game1.graphics.ApplyChanges();
                    }
                    break;

                case GameState.Option:
                    goto case GameState.Menu;


                case GameState.Game:
                    debug.Update(gameTime);
                    MediaPlayer.Stop();

                    Match.Update(gameTime, spriteBatch, viseur, ListToDraw);

                    viseur.Update(gameTime, ListToDraw, spriteBatch);
                    instance.Volume = 0.4f;

                    if (Inputs.Keyr(Keys.Escape)) goto case GameState.Exit;
                    break;


                case GameState.GameStart:
                    LoadContent();
                    currentGameState = GameState.Game;
                    debug.Update(gameTime);
                    break;


                case GameState.Exit:
                    UnloadContent();
                    Exit();
                    base.Update(gameTime);
                    return;

                default:
                    break;
            }

            
            

            //if (menu.InGame)
            //{
            //    MediaPlayer.Stop();

            //    viseur.Update(gameTime, ListToDraw, spriteBatch);
            //    instance.Volume = 0.4f;

            //    Match.Update(gameTime, spriteBatch);

            //    if (Inputs.Keyr(Keys.Escape))
            //    {
            //        Exit();
            //        base.Update(gameTime);
            //        return;
            //    }
            //}
            //else // VIDE INTERSIDERAL
            //{
            //    menu.Update(currentGameState, gameTime);

            //    //if (menu.IsExit) { base.EndRun(); base.Exit(); base.Update(gameTime); return; }
            //    //checkExitKey(curKey);

            //    if (Inputs.isLMBClick()) click.Play();
            //}



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
