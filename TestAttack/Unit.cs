using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBullet;

namespace TestAttack
{
    public class Unit : Sprite
    {

        Bullet _bullet;
        public void Follow(Sprite followTarget)
        {
            FollowTarget = followTarget;
            if (FollowTarget == null)
                return;

            var distance = FollowTarget.Position - this.Position;
            Rotation = (float)Math.Atan2(distance.Y, distance.X);

           
        }
        public Unit(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime theTime)
        {
          //  _bullet.Shoot(FollowTarget);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, Origin, 1f, SpriteEffects.None, Layer);
        }
    }
}
