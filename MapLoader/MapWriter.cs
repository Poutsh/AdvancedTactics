using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using TWrite = MapLoader.

namespace MapLoader
{
    [ContentTypeWriter]
    public class MapWriter : ContentTypeWriter<TWrite>
    {
        protected override void Write(ContentWriter output, TWrite value)
        {
            output.Write(value.Rows);
            output.Write(value.Columns);
            for (int row = 0; row < value.Rows; row++)
            {
                for (int column = 0; column < value.Columns; column++)
                {
                    output.Write(value.GetValue(row, column));
                }
            }
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "MapLibrary.MapReader, MapLibrary";
        }
    }
}
