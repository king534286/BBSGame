using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BBSGame
{
    public class Player
    {
        Texture2D texture;
        public Vector2 Position { get; private set; }
        float speed = 150;

        public Player(ContentManager content)
        {
            LoadContent(content);
            Position = new Vector2(100, 100);
        }

        private void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("C:/Users/L/source/repos/BBSGame/Content/PlayerPoses/soldier_idle");

            Position = new Vector2(100, 100);
        }

        public void Update(GameTime gameTime, Level level)
        {
            var keyboardState = Keyboard.GetState();
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 newPosition = Position;

            if (keyboardState.IsKeyDown(Keys.Left))
                newPosition.X -= speed * delta;
            if (keyboardState.IsKeyDown(Keys.Right))
                newPosition.X += speed * delta;
            if (keyboardState.IsKeyDown(Keys.Up))
                newPosition.Y -= speed * delta;
            if (keyboardState.IsKeyDown(Keys.Down))
                newPosition.Y += speed * delta;

            if (!IsCollidingWithLevel(level, newPosition))
            {
                Position = level.ClampToLevelBounds(newPosition);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        private bool IsCollidingWithLevel(Level level, Vector2 newPosition)
        {
            // Calculate the new bounding box for the player at the new position
            Rectangle newPlayerBounds = new Rectangle((int)newPosition.X, (int)newPosition.Y, texture.Width, texture.Height);

            // Iterate over the level tiles
            for (int y = 0; y < level.levelData.GetLength(0); y++)
            {
                for (int x = 0; x < level.levelData.GetLength(1); x++)
                {
                    int tileIndex = level.levelData[y, x];

                    // If the tile is solid (e.g., tileIndex 1, adjust as needed for your level data)
                    if (tileIndex == 1)
                    {
                        // Calculate the tile's bounding box
                        Rectangle tileBounds = new Rectangle(x * level.TileWidth, y * level.TileHeight, level.TileWidth, level.TileHeight);

                        // Check if the new player bounds intersect with the tile bounds
                        if (newPlayerBounds.Intersects(tileBounds))
                        {
                            return true; // Collision detected
                        }
                    }
                }
            }

            return false; // No collision detected
        }
    }
}
