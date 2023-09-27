
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
    public class Enemy : Sprite
    {
        public Enemy(Texture2D texture) : base(texture)
        {
        }
       

        public override void Update(GameTime theTime)
        {
            base.Update(theTime);
        }


    }
}
