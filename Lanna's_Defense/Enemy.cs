using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanna_s_Defense
{
    internal class Enemy
    {
        public Texture2D monsterTexture { get; set; }
       

        public List<String> instructions = new List<string>();
        public Instructor instruction1;
        public double GameT;
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        public int Health { get; set; }
        public int DirX { get; set; }
        public int DirY { get; set; }
        public Vector2 Dir;

        public string type { get; set; }
        private double timeSinceLast = 0;
        private double gt;

        public Enemy(List<String> instructions, Vector2 position, float speed, double gameT, int Health, Texture2D monsterTexture, string type)
        {
            this.instructions = instructions;
            this.Position = position;
            this.Speed = speed;
            this.GameT = gameT;
            this.Health = Health;
            this.monsterTexture = monsterTexture;
            this.type = type;
        }

        public void Start()
        {
            instruction1 = new Instructor(instructions, GameT);
            instruction1.getTime();
        }
        public void UpdateEnemy()
        {
            if (GameT > timeSinceLast + 300)
            {
                if (monsterTexture == Game1.monsterHit)
                {
                    monsterTexture = Game1.changeMonster1Texture(monsterTexture, type);
                }

                timeSinceLast = GameT;
            }
        }

        public void TakeDamage(int dmg)
        {
            Health -= dmg;
            monsterTexture = Game1.changeMonster1Texture(monsterTexture, type);
           
        }
        public void changeDir()
        {
            UpdateEnemy();
            instruction1.SetGameTime(GameT);
            DirX = instruction1.dx;
            DirY = instruction1.dy;
            Dir = new Vector2(DirX, DirY);
            instruction1.CreateInstructions(GameT);
        }
    }
}
