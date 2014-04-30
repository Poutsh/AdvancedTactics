using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.IO;

namespace Advanced_Tactics
{
    public class TileEngine
    {
        #region VARIABLES

        Data data;
        List<Texture2D> tiles;
        Sprite eau = new Sprite();
        Sprite terre = new Sprite();
        Sprite montagne = new Sprite();
        int[,] tileMap;
        SpriteFont font;
        int width, height;
        Map map;

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEUR

        public TileEngine(string name, Data data, Map carte)
        {
            this.data = data;
            map = carte;
            LoadTileTextures(data.Content);
            LoadMapData(name);
        }

        #endregion

        // // // // // // // // 

        #region DRAW

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < tileMap.GetLength(0); y++)
            {
                for (int x = 0; x < tileMap.GetLength(1); x++)
                {
                    spriteBatch.Draw(tiles[tileMap[y, x]], new Vector2(data.PosXInit+(x * data.TileSize * data.Scale), 1+y * data.Scale * data.TileSize), null, Color.White, 0, Vector2.Zero, data.Scale, SpriteEffects.None, 1);
                }
            }
        }

        void LoadTileTextures(ContentManager content)
        {
            tiles = new List<Texture2D>();
            tiles.Add(content.Load<Texture2D>("Map/0 eau"));
            tiles.Add(content.Load<Texture2D>("Map/1 terre"));
            tiles.Add(content.Load<Texture2D>("Map/2 montagne"));

            this.font = content.Load<SpriteFont>("font");
        }

        void LoadMapData(string name)
        {
            // Width and height of our tile array
            width = 0;
            height = File.ReadLines(data.fileMap).Count();
            
            tileMap = new int[height, width];
            StreamReader sReader = new StreamReader(data.fileMap);
            string line = sReader.ReadLine();
            string[] tileNo = line.Split(',');

            width = tileNo.Count();

            // Creating a new instance of the tile map
            tileMap = new int[height, width];
            sReader.Close();

            // Re-initialising sReader
            sReader = new StreamReader(data.fileMap);
            
            for (int y = 0; y < height; y++)
            {
                line = sReader.ReadLine();
                tileNo = line.Split(',');

                for (int x = 0; x < width; x++)
                {
                    tileMap[y, x] = Convert.ToInt32(tileNo[x]);
                    data.altitudeTerrain[x,y] = Convert.ToInt32(tileNo[x]);
                }
            }
            sReader.Close();
        }

        #endregion
    }
}
