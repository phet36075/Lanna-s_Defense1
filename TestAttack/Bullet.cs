using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TestAttack;

namespace TestBullet
{
    internal class Bullet : Sprite
    {
        public Bullet(Texture2D texture) : base(texture)
        {

        }
        public int timer = 60;
        public bool Ishit;
        public bool Lasthit = false;
        public int Counter =0;
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
            CollisionCheck();

        }
        public override void Update(GameTime theTime)
        {
            
            Console.WriteLine(timer);
            base.Update(theTime);
        }
        public void CollisionCheck()
        {

            Rectangle BulletRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, 32, 48);
            Rectangle PlayerRectangle = new Rectangle((int)FollowTarget.Position.X, (int)FollowTarget.Position.Y, 32, 48);
            if (BulletRectangle.Intersects(PlayerRectangle))
            {
                Ishit = true;
                Console.WriteLine("CounterBefore = " + Counter);
                if (Counter < Game1.maxEnemy-1 )
                    {
                    this.Position = new Vector2(600, 600);
                    Counter +=1;
                        Console.WriteLine("CounterAfter = " + Counter);
                    }
                    else if(Counter == Game1.maxEnemy-1 )
                {
                    Lasthit = true;
                    //this.Position = new Vector2(200, 500);
                }
                   
                       
                    

                


            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, Origin, 1f, SpriteEffects.None, Layer);
        }



    }
}
