using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace BBSGame
{
    public class Level
    {
        public int[,] levelData;
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        private Texture2D tileTexture;

        public Level(int tileWidth, int tileHeight)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        public void LoadContent(GraphicsDevice graphicsDevice, string levelPath, string tilePath)
        {
            // Load the tile texture
            using (var stream = new FileStream(tilePath, FileMode.Open))
            {
                tileTexture = Texture2D.FromStream(graphicsDevice, stream);
            }

            // Load the level data from a text file
            string[] levelLines = File.ReadAllLines(levelPath);
            int levelHeight = levelLines.Length;
            int levelWidth = levelLines[0].Length;

            levelData = new int[levelHeight, levelWidth];

            for (int y = 0; y < levelHeight; y++)
            {
                for (int x = 0; x < levelWidth; x++)
                {
                    int.TryParse(levelLines[y][x].ToString(), out levelData[y, x]);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < levelData.GetLength(0); y++)
            {
                for (int x = 0; x < levelData.GetLength(1); x++)
                {
                    int tileIndex = levelData[y, x];
                    if (tileIndex != 0)
                    {
                        spriteBatch.Draw(tileTexture, new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight), Color.White);
                    }
                }
            }
        }

        public Vector2 ClampToLevelBounds(Vector2 position)
        {
            int maxX = TileWidth * levelData.GetLength(1) - TileWidth;
            int maxY = TileHeight * levelData.GetLength(0) - TileHeight;

            return new Vector2(MathHelper.Clamp(position.X, 0, maxX), MathHelper.Clamp(position.Y, 0, maxY));
        }
    }
}
