﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using AdvancedLibrary;

namespace Advanced_Tactics
{
    public class Sprite
    {
        private Variable var = Game1.var;
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        private Texture2D _texture;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private Vector2 _position;

        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = Vector2.Normalize(value); }
        }
        private Vector2 _direction;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        private float _speed;

        public virtual void Initialize()
        {
            _position = Vector2.Zero;
            _direction = Vector2.Zero;
            _speed = 0;
        }

        public virtual void LoadContent(ContentManager content, string assetName)
        {
            _texture = content.Load<Texture2D>(assetName);
        }

        public virtual void Update(GameTime gameTime)
        {
            _position += _direction * _speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public virtual void HandleInput(KeyboardState keyboardState, MouseState mouseState)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 posinit)
        {
            spriteBatch.Draw(_texture, posinit, null, Color.White, 0, Vector2.Zero, var.Scale, SpriteEffects.None, 1);
        }
    }
}
