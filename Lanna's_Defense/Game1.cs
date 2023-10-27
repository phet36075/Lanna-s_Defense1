using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
// using System.Formats.Asn1;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.Collections.Generic;
using _321_Lab05_3;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using SharpDX.DirectWrite;


namespace Lanna_s_Defense
{
    public class Game1 : Game
    {
        List<String> path1 = new List<string>() { "r3.45" };
        List<String> path1_heavy = new List<string>() { "r3.45" };
        List<String> path1_fast = new List<string>() { "r3.45" };

        List<String> path2 = new List<string>() { "l0.5", "u3.5", "r3.2", "d3.5", "r0.5" }; 
        List<String> path2_fast = new List<string>() { "l0.5", "u1.75", "r1.9", "d1.75", "r0.25" };

        List<String> path3 = new List<string>() { "l0.5", "d3.75", "r3", "u3.7", "r0.5" }; 
        List<String> path3_fast = new List<string>() { "l0.5", "d2", "r1.8", "u2", "r0.25" };

        List<String> path4 = new List<string>() { "l0.5", "u3.5", "r5.5", "d3.5", "r0.5" }; 
        List<String> path4_fast = new List<string>() { "l0.5", "u1.75", "r3.25", "d1.75", "r0.25" };

        List<String> path5 = new List<string>() { "l0.5", "d3.5", "r5.2", "u3.5", "r0.5" };
        List<String> path5_fast = new List<string>() { "l0.5", "d1.75", "r3.15", "u1.75", "r0.25" };

        static List<Enemy> enemyList = new List<Enemy>();
        static List<Turret> turretList = new List<Turret>();

        Song main, ingame;

        double gt = 0;
        static double staticGt = 0;
        double timeSinceLast = 0;
        int enemiesKilled = 0;
        public int dx = 0;
        public int dy = 0;

        private SpriteFont font;
        public static int score = 2000;
        Texture2D backgroundPath1Texture;
        Texture2D background1Texture;

        // Texture2D monster1Texture;
        static public SpriteSheet soldierSheet;
        static public SpriteSheet horseSheet;
        static public SpriteSheet elephantSheet;

        static public AnimatedSprite soldier;
        static public AnimatedSprite horse;
        static public AnimatedSprite elephant;

        static public SpriteSheet soldierSheetHit;
        static public SpriteSheet horseSheetHit;
        static public SpriteSheet elephantSheetHit;

        static public AnimatedSprite soldierHit;
        static public AnimatedSprite horseHit;
        static public AnimatedSprite elephantHit;


        Texture2D turretBaseTexture;
        static Texture2D basicTurretIdle;
        static Texture2D basicTurretShoot;
        Texture2D rangeTexture;

        Texture2D moneyCounterTexture;

        Texture2D cardBasicTurret;
        static Texture2D shootSpeedUpgrade;
        static Texture2D rangeUpgrade;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseController mouseController;
        public static GraphicsDeviceManager static_graphics;

        Texture2D whiteRectangle;


        int playerHealth = 5;
        int spawnAmount = 10;
        int spawnRate = 1500;
        int higherMonsterChance = 55;
        double tempe;
        

        Texture2D menuTexture;
        Texture2D gameplayTexture;
        ScreenState mCurrentScreen;
        enum ScreenState
        {
            Title,
            Gameplay,

        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            static_graphics = _graphics;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
        }
        protected override void LoadContent()
        {


            _spriteBatch = new SpriteBatch(GraphicsDevice);
            mCurrentScreen = ScreenState.Title;
            font = Content.Load<SpriteFont>("Gamefont");
            background1Texture = Content.Load<Texture2D>("Map/Map1");

            this.main = Content.Load<Song>("Sound/maintheme");
            this.ingame = Content.Load<Song>("Sound/ingametheme");
            MediaPlayer.Play(main);
            MediaPlayer.IsRepeating = true;

            cardBasicTurret = Content.Load<Texture2D>("UI/CardBasicturret");
            shootSpeedUpgrade = Content.Load<Texture2D>("UI/ShootSpeedUpgrade");
            rangeUpgrade = Content.Load<Texture2D>("UI/RangeUpgrade");

            SpriteSheet soldierSheet = Content.Load<SpriteSheet>("Enemies/myanmarsoldieranimation.sf", new JsonContentLoader());
            SpriteSheet horseSheet = Content.Load<SpriteSheet>("Enemies/myanmarhorseanimation.sf", new JsonContentLoader());
            SpriteSheet elephantSheet = Content.Load<SpriteSheet>("Enemies/myanmarelephantanimation.sf", new JsonContentLoader());

            soldier = new AnimatedSprite(soldierSheet);
            horse = new AnimatedSprite(horseSheet);
            elephant = new AnimatedSprite(elephantSheet);

            SpriteSheet soldierSheetHit = Content.Load<SpriteSheet>("Enemies/myanmarsoldieranimationhit.sf", new JsonContentLoader());
            SpriteSheet horseSheetHit = Content.Load<SpriteSheet>("Enemies/myanmarhorseanimationhit.sf", new JsonContentLoader());
            SpriteSheet elephantSheetHit = Content.Load<SpriteSheet>("Enemies/myanmarelephantanimationhit.sf", new JsonContentLoader());

            soldierHit = new AnimatedSprite(soldierSheetHit);
            horseHit = new AnimatedSprite(horseSheetHit);
            elephantHit = new AnimatedSprite(elephantSheetHit);


            turretBaseTexture = Content.Load<Texture2D>("Units/Base");
            moneyCounterTexture = Content.Load<Texture2D>("UI/MoneyCounter");
            rangeTexture = Content.Load<Texture2D>("Units/RangeTexture");

            basicTurretIdle = Content.Load<Texture2D>("Units/BasicTurret");
            basicTurretShoot = Content.Load<Texture2D>("Units/BasicTurretShoot");

            whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });

            mouseController = new MouseController(turretList, turretBaseTexture.Width, turretBaseTexture.Height, gt);

            this.IsMouseVisible = true;
        }
        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume

            MediaPlayer.Play(main);
            MediaPlayer.Volume = 1.0f;
        }
        void MediaPlayer_MediaStateChanged2(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume

            MediaPlayer.Play(ingame);
            MediaPlayer.Volume = 1.0f;
        }
        public static AnimatedSprite changeMonster1Texture(AnimatedSprite current, string type)
        {
            if (current == soldier)          
            {
                return soldierHit;             
            }
            if (current == horse)
            {
                return horseHit;
            }
            if (current == elephant)
            {
                return elephantHit;
            }
            else
            {
                switch (type)
                {
                    case "normal":
                        return soldier;
                    case "heavy":
                        return elephant;
                    case "fast":
                        return horse;
                    default:
                        return soldier;
                }

            }
        }
        public static Texture2D changeTurretTexture(Texture2D current, int e)
        {
            if (e == 1)
            {
                return basicTurretIdle;
            }
            else if (current == basicTurretIdle)
            {
                return basicTurretShoot;
            }
            else
            {
                return basicTurretIdle;
            }

        }


        void WaveManager()
        {
            if (gt > timeSinceLast + spawnRate)
            {
                if (enemyList.Count + enemiesKilled < spawnAmount)
                {
                    AddEnemy();
                    timeSinceLast = gt;
                }
                else
                {
                    NextWave();
                }
            }
        }
        void NextWave()
        {
            if (enemiesKilled == spawnAmount)
            {
                Console.WriteLine("Next Wave!");
                spawnAmount += 10;
                if (higherMonsterChance != -40)
                {
                    higherMonsterChance -= 20;
                }
                enemiesKilled = 0;
                if (spawnRate >= 1000)
                {
                    spawnRate -= 500;
                }
                timeSinceLast = gt;
            }
        }



        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            

            switch (mCurrentScreen)
            {
                case ScreenState.Title:
                    {
                        UpdateTitle(); break;
                    }
                case ScreenState.Gameplay:
                    {
                        UpdateGameplay();
                        mouseController.MouseUpdate();

                        mouseController.gt = gt;
                        WaveManager();

                        gt = gameTime.TotalGameTime.TotalMilliseconds;
                        staticGt = gt; break;

                    }
            }
           

            if (enemyList.Count > 0)
            {
                foreach (Enemy enemy in enemyList)
                {
                    enemy.GameT = gt;
                    enemy.changeDir();
                    float enemyX = enemy.Position.X;
                    float enemyY = enemy.Position.Y;
                    enemyX += enemy.Speed * enemy.DirX * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    enemyY += enemy.Speed * enemy.DirY * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    enemy.Position = new Vector2(enemyX, enemyY);
                    
                    soldier.Play("goRight");                 
                    elephant.Play("goRight"); 
                    horse.Play("goRight");                 
                    soldierHit.Play("rightHit");               
                    elephantHit.Play("rightHit");               
                    horseHit.Play("rightHit");
                    soldier.Update(gameTime);
                    elephant.Update(gameTime);
                    horse.Update(gameTime);
                    soldierHit.Update(gameTime);
                    elephantHit.Update(gameTime);
                    horseHit.Update(gameTime);



                }
                for (int i = 0; i < enemyList.Count; i++)
                {
                    
                    if (enemyList[i].Health <= 0)
                    {
                        EnemyDie(i);
                        break;
                    }
                    
                    
                    if (enemyList[i].Position.X > _graphics.PreferredBackBufferWidth)
                    {
                        Console.WriteLine("you took damage");
                        tempe = gt;
                        if (playerHealth <= 0)
                        {

                            tempe = gt;
                            Console.WriteLine("You died");

                        }
                        else
                        {

                            playerHealth--;
                        }
                        EnemyDie(i);
                    }

                }

            }
            foreach (Turret turret in turretList)
            {
                turret.enemies = enemyList;
                turret.EnemyUpdate();
                turret.SetGameTime(staticGt);
            }
            
            base.Update(gameTime);
        }
        void AddEnemy()
        {
            Vector2 pos = new Vector2(-64 / 2, (_graphics.PreferredBackBufferHeight / 2) - 30);
            Vector2 pos2 = new Vector2(-64 / 2, (_graphics.PreferredBackBufferHeight / 2) - 50);
            Vector2 pos3 = new Vector2(-64 / 2, (_graphics.PreferredBackBufferHeight / 2) - 120);
            int num = getRandomNum();
            if (num >= 75 - higherMonsterChance)
            {
                Enemy enemy = new Enemy(path1, pos, 120f, gt, 3, soldier, "normal");
                enemyList.Add(enemy);
                
                enemy.Start();
            }
            else if (num >= 54 - higherMonsterChance)
            {
                Enemy enemy = new Enemy(path1_heavy, pos3, 30f, gt, 100, elephant, "heavy");
                enemyList.Add(enemy);
                enemy.Start();
            }
            else
            {
                Enemy enemy = new Enemy(path1_fast, pos2, 240f, gt, 7, horse, "fast");
                enemyList.Add(enemy);
                enemy.Start();
            }
            if (num >= 55 - higherMonsterChance)
            {
                Enemy enemy = new Enemy(path2, pos, 130f, gt, 3, soldier, "normal");
                enemyList.Add(enemy);
                enemy.Start();
            }
            else if (num >= 34 - higherMonsterChance) 
            {
                Enemy enemy = new Enemy(path2_fast, pos2, 250f, gt, 7, horse, "fast");
                enemyList.Add(enemy);
                enemy.Start();
            }
            if (num >= 55 - higherMonsterChance)
            {
                Enemy enemy = new Enemy(path3, pos, 140f, gt, 3, soldier, "normal");
                enemyList.Add(enemy);
                enemy.Start();
            }
            else if (num >= 34 - higherMonsterChance)
            {
                Enemy enemy = new Enemy(path3_fast, pos2, 260f, gt, 7, horse, "fast");
                enemyList.Add(enemy);
                enemy.Start();
            }
            if (num >= 55 - higherMonsterChance)
            {
                Enemy enemy = new Enemy(path4, pos, 150f, gt, 3, soldier, "normal");
                enemyList.Add(enemy);
                enemy.Start();
            }
            else if (num >= 34 - higherMonsterChance) 
            {
                Enemy enemy = new Enemy(path4_fast, pos2, 270f, gt, 7, horse, "fast");
                enemyList.Add(enemy);
                enemy.Start();
            }
            if (num >= 55 - higherMonsterChance)
            {
                Enemy enemy = new Enemy(path5, pos, 160f, gt, 3, soldier, "normal");
                enemyList.Add(enemy);
                enemy.Start();
            }
            else if (num >= 34 - higherMonsterChance)
  
            {
                Enemy enemy = new Enemy(path5_fast, pos2, 280f, gt, 7, horse, "fast");
                enemyList.Add(enemy);
                enemy.Start();
            }

        }
        int getRandomNum()
        {
            Random random = new Random();
            int num = random.Next(1, 100);
            return num;
        }
        public static void AddTurret(Vector2 position)
        {
            if (score >= 500)
            {
                score -= 500;
                Vector2 pos = position;
                UpgradeCard shootUppgradeCard = new UpgradeCard(shootSpeedUpgrade, new Vector2(static_graphics.PreferredBackBufferWidth - 232, static_graphics.PreferredBackBufferHeight / 2 + 55), 0f, new Vector2(32, 32));
                UpgradeCard rangeUppgradeCard = new UpgradeCard(rangeUpgrade, new Vector2(static_graphics.PreferredBackBufferWidth - 232, static_graphics.PreferredBackBufferHeight / 2 + 170), 0f, new Vector2(32, 32));
                Turret turret = new Turret(position, enemyList, 125f, staticGt, 240, basicTurretIdle, shootUppgradeCard, rangeUppgradeCard);

                turretList.Add(turret);
            }

        }
        public static void DeleteTurret(int turretIndex)
        {
            turretList.RemoveAt(turretIndex);
            score += 200;
        }



        void EnemyDie(int index)
        {          
            enemiesKilled++;
            score += 50;
            enemyList.RemoveAt(index);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            

            switch (mCurrentScreen)
            {
                case ScreenState.Title:
                    {
                        DrawMenu(); 
                        break;
                    }
                case ScreenState.Gameplay:
                    {
                        DrawGameplay(); 
                        break;
                    }
            }
            base.Draw(gameTime);
        }

        
        void DrawTexture(Texture2D texture, Vector2 position, float rotation, Vector2 offset, Vector2 scale)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                texture,
                position,
                null,
                Color.White,
                rotation,
                offset,
                scale,
                SpriteEffects.None,
                0f
            );


            _spriteBatch.End();
        }

        void DrawFrame(AnimatedSprite enemy, Vector2 position, float rotation, Vector2 offset, Vector2 scale)
        {
            _spriteBatch.Begin();
            enemy.Draw(_spriteBatch, position, rotation, scale);
            

            _spriteBatch.End();
        }
        void DrawShape()
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(
                whiteRectangle,
                new Vector2(20f, 20f),
                null,
                Color.Chocolate,
                0f,
                Vector2.Zero,
                new Vector2(80f, 80f),
                SpriteEffects.None, 0f
            );
            _spriteBatch.End();
        }
        void DrawText(SpriteFont spriteFont, Vector2 position, String content)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(
                font,
                content,
                position,
                Color.White
            );
            _spriteBatch.End();
        }

        private void DrawMenu()
        {
           
        }
        private void DrawGameplay()
        {
            DrawTexture(background1Texture, new Vector2(32, 32), 0, new Vector2(32, 32), Vector2.One);
            DrawTexture(moneyCounterTexture, new Vector2(_graphics.PreferredBackBufferWidth / 40, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height / 2), 0, new Vector2(32, 32), Vector2.One);
            DrawText(font, new Vector2(_graphics.PreferredBackBufferWidth / 40, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height + 25), score.ToString());
            DrawText(font, new Vector2(_graphics.PreferredBackBufferWidth / 2, 10), playerHealth.ToString());

            if (playerHealth <= 0)
            {
                DrawText(font, new Vector2(-50 + _graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), "YOU DIED");
                if (gt > tempe + 3000)
                {
                    this.Exit();
                }
            }

            foreach (Enemy enemy in enemyList)
            {
                DrawFrame(enemy.monsterTexture, enemy.Position, 0, new Vector2(32, 360), Vector2.One);
            }

            foreach (Turret turret in turretList)
            {
                DrawTexture(turretBaseTexture, turret.position, 0, new Vector2(32, 32), Vector2.One);
                DrawTexture(turret.basicTurretTexture, turret.position, turret.rotation, new Vector2(32, 32), Vector2.One);



                if (turret.showUpgrades)
                {
                    DrawTexture(cardBasicTurret, new Vector2(_graphics.PreferredBackBufferWidth - cardBasicTurret.Width, -cardBasicTurret.Height / 2 + _graphics.PreferredBackBufferHeight / 2), 0f, new Vector2(32, 32), Vector2.One);
                    DrawTexture(turret.shootUppgrade.texture, turret.shootUppgrade.position, turret.shootUppgrade.rotation, turret.shootUppgrade.offset, Vector2.One);
                    DrawTexture(turret.rangeUppgrade.texture, turret.rangeUppgrade.position, turret.rangeUppgrade.rotation, turret.rangeUppgrade.offset, Vector2.One);
                    DrawTexture(rangeTexture, new Vector2(turret.position.X - (turret.rangeTextureScale * rangeTexture.Width / 2), turret.position.Y - (turret.rangeTextureScale * rangeTexture.Height / 2)), 0, new Vector2(0f, 0f), new Vector2(turret.rangeTextureScale, turret.rangeTextureScale));
                    // Console.WriteLine(turret.rangeTextureScale);
                }
            }
        }

        private void UpdateGameplay()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A) == true)
            {
                mCurrentScreen = ScreenState.Title;
            }

        }
        private void UpdateTitle()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.B) == true)
            {
                mCurrentScreen = ScreenState.Gameplay;
            }

        }
    }
}
