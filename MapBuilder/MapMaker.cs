using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using Screens.Handling;

namespace MapBuilder
{
    public class MapMaker
    {
        #region private members

        private ScreenRequirements screenRequirements;

        private Texture2D grassTile;
        private Texture2D waterTile;
        private Texture2D sandTile;

        private Texture2D errorTile;

        #endregion

        public MapMaker(ScreenRequirements screenRequirements)
        {
            this.screenRequirements = screenRequirements;
            grassTile = screenRequirements.content.Load<Texture2D>("Tiles/BasicGrass");
            waterTile = screenRequirements.content.Load<Texture2D>("Tiles/BasicWater");
            sandTile = screenRequirements.content.Load<Texture2D>("Tiles/BasicSand");

            errorTile = screenRequirements.content.Load<Texture2D>("Tiles/ErrorTile");
        }

        public Texture2D[,] ReadAreaMapColors()
        {
            var map = Directory.GetCurrentDirectory() + "\\AreaMap.bmp";

            using var bitmap = new Bitmap(map);
            int width = bitmap.Width;
            int height = bitmap.Height;
            var tiles = new Texture2D[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = PickTile(bitmap.GetPixel(x, y));
                }
            }
            return tiles;
        }

        private Texture2D PickTile(Color colour)
        {
            switch (colour)
            {
                case var c when c.R == 38 && c.G == 127 && c.B == 0:
                    return grassTile;
                case var c when c.R == 0 && c.G == 38 && c.B == 255:
                    return waterTile;
                case var c when c.R == 255 && c.G == 216 && c.B == 0:
                    return sandTile;
                default:
                    return grassTile;
            }
        }
    }
}
