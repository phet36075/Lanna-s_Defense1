using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestBullet
{
    public class Player : Sprite
    {
        public Player(Texture2D texture) : base(texture)
        {
        }

        public int speed = 3;
        public override void Update(GameTime theTime)
        {
            base.Update(theTime);
        }
        public void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.Position.Y += speed; 
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.Position.Y -= speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.Position.X += speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.Position.X -= speed;
            }
        }
    }
}
