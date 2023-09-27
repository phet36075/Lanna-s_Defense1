using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;


namespace TestBullet
{
    internal class Bullet : Sprite
    {
        public Bullet(Texture2D texture) : base(texture)
        {

        }

        public override void Shoot(Sprite followTarget)
        {
            FollowTarget = followTarget;
            if (FollowTarget == null)
                return;

            var distance = FollowTarget.Position - this.Position;
            Rotation = (float)Math.Atan2(distance.Y, distance.X);

            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            var currentDistance = Vector2.Distance(this.Position, FollowTarget.Position);
            if (currentDistance > FollowDistance)
            {
                var t = MathHelper.Min((float)Math.Abs(currentDistance - FollowDistance), LinearVelocity);
                var velocity = Direction * t;

                Position += velocity;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, Origin, 1f, SpriteEffects.None, Layer);
        }



    }
}
