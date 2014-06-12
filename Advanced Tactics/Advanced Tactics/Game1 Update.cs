using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Advanced_Tactics
{
    public partial class Game1 : Microsoft.Xna.Framework.Game
    {
        protected override void Update(GameTime gameTime)
        {
            // Init entrees utilisateur
            Inputs.Update();
            sppointer.Update(gameTime);

            menu.InGame = true;

            if (menu.InGame)
            {
                MediaPlayer.Stop();

                viseur.Update(gameTime, ListToDraw, spriteBatch);
                instance.Volume = 0.4f;

                foreach (Player Player in Players)
                {
                    Player.PosHQ(viseur, map, Data, ListToDraw);
                }

                if (Inputs.Keyr(Keys.Escape))
                {
                    Exit();
                    base.Update(gameTime);
                    return;
                }
            }
            else // VIDE INTERSIDERAL
            {
                menu.Update(gameTime);

                //if (menu.IsExit) { base.EndRun(); base.Exit(); base.Update(gameTime); return; }
                //checkExitKey(curKey);

                if (Inputs.isLMBClick()) click.Play();
            }

            if (!menu.InGame && !menu.MenuPrincipal && menu.Options)
            {
                graphics.PreferredBackBufferWidth = (int)Data.widthWindow;
                graphics.PreferredBackBufferHeight = (int)Data.heightWindow;
                graphics.IsFullScreen = menu.Fullscreen;
                Game1.graphics.ApplyChanges();
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
