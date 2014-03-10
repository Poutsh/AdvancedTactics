using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Advanced_Tactics
{
    public abstract class Sprites
    {
		private Game game1;
		
        Texture2D texture;
        Rectangle rectangle;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Rectangle Rectangle
        {
            get { return rectangle; }
        }

        public Sprites(Point position)
        {
            rectangle.X = position.X;
            rectangle.Y = position.Y;

            //return rectangle;
        }

        public virtual void LoadContent(ContentManager Content, String fileName)
        {
            texture = Content.Load<Texture2D>(fileName);
            rectangle.Width = texture.Width;
            rectangle.Height = texture.Height;
        }

        public void MoveTo(Point point)
        {
            rectangle.X = point.X;
            rectangle.Y = point.Y;
        }
		
		public Game Game
        {
            get { return game1; }
        }

        public ContentManager Content
        {
            get { return game1.Content; }
        }
		
		public Sprites()
        {
			
        }
    }

}