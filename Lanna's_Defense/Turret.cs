using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _321_Lab05_3;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;


namespace Lanna_s_Defense
{
    internal class Turret
    {
        public AnimatedSprite basicTurretTexture {get; set;}
        public bool mouseIsHovering = false;
        public bool beenPressed = false;
        public Vector2 position {get; set;}
        public List<Enemy> enemies = new List<Enemy>();
        public float turretRange {get; set;}
        List<double> allDistances = new List<double>();
        public float rotation;
        public int shootRate {get; set;}
        double timeSinceLast = 0;
        double animTime = 0;
        public double gt {get; set;}
        private double currentClosest = 10000;

        float lookDirX;
        float lookDirY;
        bool e = true;
        int p = 0;
        public int damage = 1;

        bool destroyed = true;
        Enemy closestEnemy = null;
        Enemy farthestEnemy = null;
        
        bool targetFound = false;
        bool targetEnemyKilled = false;

        public bool showUpgrades;
        public UpgradeCard shootUppgrade;
        public UpgradeCard rangeUppgrade;

        public float rangeTextureScale = 1.5f;

        public Turret(Vector2 position, List<Enemy> enemies, float turretRange, double gt, int shootRate, AnimatedSprite basicTurretTexture, UpgradeCard shootUppgrade, UpgradeCard rangeUppgrade){
            this.position = position;
            this.enemies = enemies;
            this.turretRange = turretRange;
            this.gt = gt;
            this.shootRate = shootRate;
            this.basicTurretTexture = basicTurretTexture;
            this.shootUppgrade = shootUppgrade;
            this.rangeUppgrade = rangeUppgrade;
        }
        
        public void SetGameTime(double _gt){
            gt = _gt;
        }
        public void EnemyUpdate(){
            // GetClosest();
            if(enemies.Count > 0){
                GetFarthestEnemy();
                GetRotation(farthestEnemy);
            }
            
            if (gt > timeSinceLast + shootRate)
            {
                ShootEnemy();
                timeSinceLast = gt;
            }
        }

        double calcDistance(Vector2 pos1, Vector2 pos2){
            float yDistance = pos2.Y - pos1.Y;
            float xDistance = pos2.X - pos1.X;

            double pyt = (xDistance * xDistance) + (yDistance * yDistance);
            double distance = Math.Sqrt(pyt);
            return distance;
        }
        void GetFarthestEnemy(){
            if(targetEnemyKilled == true || farthestEnemy == null){
                //Search new Enemy
                for (int i = 0; i < enemies.Count; i++)
                {
                    double distanceToEnemy = calcDistance(position, enemies[i].Position);
                    if(distanceToEnemy < turretRange){
                        //Within range of turret
                        farthestEnemy = enemies[i];
                        targetFound = true;
                        targetEnemyKilled = false;
                        break;
                    }
                }
            }else{
                double farthestEnemyDistance = calcDistance(position, farthestEnemy.Position);
                if(farthestEnemyDistance > turretRange){
                    //farthest enemy out of range, get new target
                    targetFound = false;
                    targetEnemyKilled = true;
                }
            }
        }
        void GetRotation(Enemy target){

            if(target != null && targetFound){
                lookDirX = position.X + target.Position.X;
                lookDirY = position.Y - target.Position.Y;

                if (gt > animTime + shootRate)
                {
                    if(!e){
                        e = true;
                        Vector2 test = new Vector2(position.X + lookDirX/70, position.Y + lookDirY/70);
                        position = test;
                        basicTurretTexture = Game1.changeTurretTexture(basicTurretTexture, 0);
                        //Game1.changeTurretTexture();
                    }else{
                        e = false;
                        Vector2 test = new Vector2(position.X - lookDirX/70, position.Y - lookDirY/70);
                        position = test;
                        basicTurretTexture = Game1.changeTurretTexture(basicTurretTexture, 0);
                    }
                    animTime = gt;
                }
                rotation = (float)Math.Atan2(lookDirY, lookDirX);
            }else if(!targetFound){
                basicTurretTexture = Game1.changeTurretTexture(basicTurretTexture, 1);
            }
        }

        void GetClosest(){
            double distanceToClosestEnemy = 10000;
            closestEnemy = null;

            foreach(Enemy enemy in enemies){
                float yDistance = enemy.Position.Y - position.Y;
                float xDistance = enemy.Position.X - position.X;

                double pyt = (xDistance * xDistance) + (yDistance * yDistance);
                double distanceToEnemy = Math.Sqrt(pyt);

                if(distanceToEnemy < distanceToClosestEnemy && distanceToEnemy < turretRange){
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
            if(closestEnemy != null){

               //Console.WriteLine(closestEnemy.Position);    
            }
        }

        void ShootEnemy(){
             //if(closestEnemy != null){
                //Console.WriteLine("Shooting at " + closestEnemy.Position);
               //closestEnemy.TakeDamage(1);
            //}
            if(farthestEnemy != null){
                farthestEnemy.TakeDamage(damage);
                if(farthestEnemy.Health <= 0){
                    targetEnemyKilled = true;
                    targetFound = false;
                }
            }
        }
    }
}