using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestBullet
{
    public class Sprite
    {
        public Texture2D _texture;
        public float Rotation;
        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 Direction;
        public float FollowDistance { get; set; }
        public Sprite FollowTarget { get; set; }

        public float RotationVelocity = 3f;

        public float LinearVelocity = 4f;
        public float Layer;
        public Sprite(Texture2D texture) 
        {
            _texture = texture;
        }

        public virtual void Shoot(Sprite followTarget) 
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

       


        /*public Sprite SetShootTarget(Sprite followTarget, float followDistance)
        {
            FollowTarget = followTarget;

            FollowDistance = followDistance;

            return this;
        }*/
    }
}
