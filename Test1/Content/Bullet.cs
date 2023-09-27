using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;


namespace Test1
{
    internal class Bullet : Sprite
    {
        public Bullet(Texture2D texture) : base(texture)
        {

        }

        public void Shoot()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, Origin, 1f, SpriteEffects.None, Layer);
        }



    }
}
