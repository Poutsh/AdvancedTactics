using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Advanced_Tactics
{
    public struct DisplayMessage
    {
        public string Message;
        public TimeSpan DisplayTime;
        public int CurrentIndex;
        public Vector2 Position;
        public string DrawMessage;
        public Color DrawColor;

        public DisplayMessage(string message, TimeSpan displayTime, Vector2 position, Color color)
        {
            Message = message;
            DisplayTime = displayTime;
            CurrentIndex = 0;
            Position = position;
            DrawMessage = string.Empty;
            DrawColor = color;
        }
    }

    public class Message
    {
        public List<DisplayMessage> Messages;
        public SpriteFont font;

        public Message()
        {
            Messages = new List<DisplayMessage>();
            font = Game1.Ctt.Load<SpriteFont>("message");
        }

        public void Update(GameTime gameTime)
        {
            if (Messages.Count > 0)
            {
                for (int i = 0; i < Messages.Count; i++)
                {
                    DisplayMessage dm = Messages[i];
                    dm.DisplayTime -= gameTime.ElapsedGameTime;
                    if (dm.DisplayTime <= TimeSpan.Zero)
                        Messages.RemoveAt(i);
                    else
                        Messages[i] = dm;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Messages.Count > 0)
            {
                for (int i = 0; i < Messages.Count; i++)
                {
                    DisplayMessage dm = Messages[i];
                    dm.DrawMessage += dm.Message[dm.CurrentIndex].ToString();
                    spriteBatch.DrawString(font, dm.DrawMessage, dm.Position, dm.DrawColor);
                    if (dm.CurrentIndex != dm.Message.Length - 1)
                    {
                        dm.CurrentIndex++;
                        Messages[i] = dm;
                    }
                }
            }
        }
    }
}
