using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

using TImport = System.String;

namespace MapLoader
{
    public class MapImporter
    {
        [ContentImporter(".level", DisplayName = "Level Importer", DefaultProcessor = "LevelProcessor")]
        public class MapImporter : ContentImporter<TImport>
        {
            public override TImport Import(string filename, ContentImporterContext context)
            {
                return System.IO.File.ReadAllText(filename);
            }
        }
    }
}
