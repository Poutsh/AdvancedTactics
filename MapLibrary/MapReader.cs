using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using TRead = MapLibrary.Map;

namespace MapLibrary
{
    public class MapReader : ContentTypeReader<TRead>
    {
        protected override TRead Read(ContentReader input, TRead existingInstance)
        {
            int rows = input.ReadInt32();
            int columns = input.ReadInt32();

            int[,] levelData = new int[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    levelData[row, column] = input.ReadInt32();
                }
            }

            return new Map(levelData);
        }
    }
}
