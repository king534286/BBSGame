using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BBSGame
{
    public class Tile
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle BoundingBox { get; set; }
        public bool IsSolid { get; set; }

        public Tile(Texture2D texture, Vector2 position, bool isSolid)
        {
            Texture = texture;
            Position = position;
            IsSolid = isSolid;
            BoundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
