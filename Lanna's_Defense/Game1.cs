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
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using SharpDX.DirectWrite;
using System.Linq;
using System.Runtime.ConstrainedExecution;


namespace Lanna_s_Defense
{
    public class Game1 : Game
    {
        #region Variable
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
        static List<Swordman> turretList1 = new List<Swordman>();
        static List<Cannon> turretList2 = new List<Cannon>();

        Song main, ingame;
        List<SoundEffect> soundEffects;
        bool IsGameOver = false;
        double gt = 0;
        static double staticGt = 0;
        double timeSinceLast = 0;
        int enemiesKilled = 0;
        public int dx = 0;
        public int dy = 0;

        private SpriteFont font;
        private SpriteFont GameMenufont;
        private SpriteFont Gameoverfont;
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
        
        static public SpriteSheet archerSheet;
        static public SpriteSheet swordmanSheet;
        static public SpriteSheet cannonSheet;

        static public AnimatedSprite archer;
        static public AnimatedSprite swordman;
        static public AnimatedSprite cannon;

        static public SpriteSheet archerSheetIdle;
        static public SpriteSheet swordmanSheetIdle;
        static public SpriteSheet cannonSheetIdle;

        static public AnimatedSprite archerIdle;
        static public AnimatedSprite swordmanIdle;
        static public AnimatedSprite cannonIdle;

        Texture2D rangeTexture;
        Texture2D rangeTexture2;
        Texture2D rangeTexture3;

        Texture2D customcursor;
        Texture2D gamelogo;
        
        Texture2D moneyCounterTexture;
        Texture2D hpCounterTexture;

        Texture2D cardBasicTurret;
        Texture2D cardBasicTurret1;
        Texture2D cardBasicTurret2;
        Texture2D cardBasicTurret3;

        static Texture2D shootSpeedUpgrade;
        static Texture2D rangeUpgrade;

        static Texture2D archerbutton;
        static Texture2D cannonbutton;
        static Texture2D swordmanbutton;
        string click = "turret";

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseController mouseController;
        public static GraphicsDeviceManager static_graphics;
        MouseState mouseState;
       
        Texture2D whiteRectangle;


        int playerHealth = 5;
        int spawnAmount = 10;
        int spawnRate = 1500;
        int higherMonsterChance = 55;
        double tempe;

        Texture2D backgroundHome;
        Texture2D menuTexture;
        Texture2D gameplayTexture;
        ScreenState mCurrentScreen;
        #endregion
        enum ScreenState
        {
            Title,
            Gameplay,
            

        }
        int state = -10;
       
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            static_graphics = _graphics;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            soundEffects = new List<SoundEffect>();

           
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            //archerbutton = new Rectangle(1020,360,70,70);
            //swordmanbutton = new Rectangle(1120,360,70,70);
            //cannonbutton = new Rectangle(1220,360,70,70);
        }
        protected override void LoadContent()
        {


            _spriteBatch = new SpriteBatch(GraphicsDevice);
            mCurrentScreen = ScreenState.Title;
            font = Content.Load<SpriteFont>("Gamefont");
            Gameoverfont = Content.Load<SpriteFont>("Gameover");
            GameMenufont = Content.Load<SpriteFont>("GameMenu");
            background1Texture = Content.Load<Texture2D>("Map/Map1");
            backgroundHome = Content.Load<Texture2D>("Map/Background");
            customcursor = Content.Load<Texture2D>("cursor");
            gamelogo = Content.Load<Texture2D>("Map/Gamelogo");

            soundEffects.Add(Content.Load<SoundEffect>("Sound/gamestart"));
            soundEffects.Add(Content.Load<SoundEffect>("Sound/gameover"));
            this.main = Content.Load<Song>("Sound/maintheme");
            this.ingame = Content.Load<Song>("Sound/ingametheme");
           

            MediaPlayer.Play(main);
            MediaPlayer.IsRepeating = true;

            archerbutton = Content.Load<Texture2D>("UI/archerbutton");
            cannonbutton = Content.Load<Texture2D>("UI/cannonbutton");
            swordmanbutton = Content.Load<Texture2D>("UI/swordmanbutton");

            cardBasicTurret = Content.Load<Texture2D>("UI/CardBasicturret");
            cardBasicTurret1 = Content.Load<Texture2D>("UI/CardBasicturret1");
            cardBasicTurret2 = Content.Load<Texture2D>("UI/CardBasicturret2");
            cardBasicTurret3 = Content.Load<Texture2D>("UI/CardBasicturret3");
            shootSpeedUpgrade = Content.Load<Texture2D>("UI/ShootSpeedUpgrade");
            rangeUpgrade = Content.Load<Texture2D>("UI/RangeUpgrade");

            SpriteSheet soldierSheet = Content.Load<SpriteSheet>("Enemies/myanmarsoldieranimation.sf", new JsonContentLoader());
            SpriteSheet horseSheet = Content.Load<SpriteSheet>("Enemies/myanmarhorseanimation.sf", new JsonContentLoader());
            SpriteSheet elephantSheet = Content.Load<SpriteSheet>("Enemies/myanmarelephantanimation.sf", new JsonContentLoader());

            soldier = new AnimatedSprite(soldierSheet);
            horse = new AnimatedSprite(horseSheet);
            elephant = new AnimatedSprite(elephantSheet);

            SpriteSheet archerSheet = Content.Load<SpriteSheet>("Unit/archeranimation.sf", new JsonContentLoader());
            SpriteSheet swordmanSheet = Content.Load<SpriteSheet>("Unit/swordmananimation.sf", new JsonContentLoader());
            SpriteSheet cannonSheet = Content.Load<SpriteSheet>("Unit/cannonanimation.sf", new JsonContentLoader());

            archer = new AnimatedSprite(archerSheet);
            swordman = new AnimatedSprite(swordmanSheet);
            cannon = new AnimatedSprite(cannonSheet);

            SpriteSheet soldierSheetHit = Content.Load<SpriteSheet>("Enemies/myanmarsoldieranimationhit.sf", new JsonContentLoader());
            SpriteSheet horseSheetHit = Content.Load<SpriteSheet>("Enemies/myanmarhorseanimationhit.sf", new JsonContentLoader());
            SpriteSheet elephantSheetHit = Content.Load<SpriteSheet>("Enemies/myanmarelephantanimationhit.sf", new JsonContentLoader());

            soldierHit = new AnimatedSprite(soldierSheetHit);
            horseHit = new AnimatedSprite(horseSheetHit);
            elephantHit = new AnimatedSprite(elephantSheetHit);

            SpriteSheet archerSheetIdle = Content.Load<SpriteSheet>("Unit/archeranimationidle.sf", new JsonContentLoader());
            SpriteSheet swordmanSheetIdle = Content.Load<SpriteSheet>("Unit/swordmananimationidle.sf", new JsonContentLoader());
            SpriteSheet cannonSheetIdle = Content.Load<SpriteSheet>("Unit/cannonanimationidle.sf", new JsonContentLoader());

            archerIdle = new AnimatedSprite(archerSheetIdle);
            swordmanIdle = new AnimatedSprite(swordmanSheetIdle);
            cannonIdle = new AnimatedSprite(cannonSheetIdle);


            turretBaseTexture = Content.Load<Texture2D>("Unit/Base");
            moneyCounterTexture = Content.Load<Texture2D>("UI/MoneyCounter");
            hpCounterTexture = Content.Load<Texture2D>("UI/HpCounter");
            rangeTexture = Content.Load<Texture2D>("Unit/RangeTexture");
            rangeTexture2 = Content.Load<Texture2D>("Unit/RangeTexture2");
            rangeTexture3 = Content.Load<Texture2D>("Unit/RangeTexture3");



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
        public static AnimatedSprite changeTurretTexture(AnimatedSprite current, int e)
        {
            if (e == 1)
            {
                return archerIdle;
            }
            else if (current == archerIdle)
            {
                return archer;
            }
            else
            {
                return archerIdle;
            }

        }
        public static AnimatedSprite changeTurretTexture1(AnimatedSprite current, int e)
        {
            if (e == 1)
            {
                return swordmanIdle;
            }
            else if (current == swordmanIdle)
            {
                return swordman;
            }
            else
            {
                return swordmanIdle;
            }

        }

        public static AnimatedSprite changeTurretTexture2(AnimatedSprite current, int e)
        {
            if (e == 1)
            {
                return cannonIdle;
            }
            else if (current == cannonIdle)
            {
                return cannon;
            }
            else
            {
                return cannonIdle;
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
                    
                    archer.Play("shootRight");
                    archer.Update(gameTime);
                    
                    if (enemy.Position.X > turretBaseTexture.Width)
                    {                   
                        archer.Play("shootLeft");
                        archer.Update(gameTime);
                    }
                    
                    archer.Update(gameTime);


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
                Turret turret = new Turret(position, enemyList, 175f, staticGt, 460, archer, shootUppgradeCard, rangeUppgradeCard);

                turretList.Add(turret);
            }
        }
        public static void AddTurret1(Vector2 position)
        {
            if (score >= 500)
            {
                score -= 500;
                Vector2 pos = position;
                UpgradeCard shootUppgradeCard = new UpgradeCard(shootSpeedUpgrade, new Vector2(static_graphics.PreferredBackBufferWidth - 232, static_graphics.PreferredBackBufferHeight / 2 + 55), 0f, new Vector2(32, 32));
                UpgradeCard rangeUppgradeCard = new UpgradeCard(rangeUpgrade, new Vector2(static_graphics.PreferredBackBufferWidth - 232, static_graphics.PreferredBackBufferHeight / 2 + 170), 0f, new Vector2(32, 32));
                Swordman turret = new Swordman(position, enemyList, 175f, staticGt, 640, swordman, shootUppgradeCard, rangeUppgradeCard);

                turretList1.Add(turret);
            }
        }
        public static void AddTurret2(Vector2 position)
        {
            if (score >= 500)
            {
                score -= 500;
                Vector2 pos = position;
                UpgradeCard shootUppgradeCard = new UpgradeCard(shootSpeedUpgrade, new Vector2(static_graphics.PreferredBackBufferWidth - 232, static_graphics.PreferredBackBufferHeight / 2 + 55), 0f, new Vector2(32, 32));
                UpgradeCard rangeUppgradeCard = new UpgradeCard(rangeUpgrade, new Vector2(static_graphics.PreferredBackBufferWidth - 232, static_graphics.PreferredBackBufferHeight / 2 + 170), 0f, new Vector2(32, 32));
                Cannon turret = new Cannon(position, enemyList, 175f, staticGt, 640, cannon, shootUppgradeCard, rangeUppgradeCard);

                turretList2.Add(turret);
            }
        }
        public static void DeleteTurret(int turretIndex)
        {
            turretList.RemoveAt(turretIndex);
            score += 200;
        }
        public static void DeleteTurret1(int turretIndex)
        {
            turretList1.RemoveAt(turretIndex);
            score += 100;
        }
        public static void DeleteTurret2(int turretIndex)
        {
            turretList2.RemoveAt(turretIndex);
            score += 300;
        }

        void EnemyDie(int index)
        {
            soundEffects[0].CreateInstance().Play();
            enemiesKilled++;

            score += 100;
            enemyList.RemoveAt(index);
            
        
        }

        #region DrawSection
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
        void DrawAnimate(AnimatedSprite unit, Vector2 position, float rotation, Vector2 offset, Vector2 scale)
        {
            _spriteBatch.Begin();
            unit.Draw(_spriteBatch, position, rotation, scale);


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

        void DrawGameover(SpriteFont spriteFont, Vector2 position, String content)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(
                Gameoverfont,
                content,
                position,
                Color.Red
            );
            _spriteBatch.End();
        }
        void DrawGamemenu(SpriteFont spriteFont, Vector2 position, String content)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(
                GameMenufont,
                content,
                position,
                Color.White
            );
            _spriteBatch.End();
        }
        void Drawcursor(Texture2D texture, Vector2 position, Color color)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                texture,
                position,
                null,
                Color.White
                
            );


            _spriteBatch.End();
        }

        private void DrawMenu()
        {
            DrawTexture(backgroundHome, new Vector2(32, 32), 0, new Vector2(32, 32), Vector2.One);
            DrawTexture(gamelogo, new Vector2(-200 + _graphics.PreferredBackBufferWidth / 2, -330 + (_graphics.PreferredBackBufferHeight / 2)), 0, new Vector2(8, 8), Vector2.One);
            DrawGamemenu(GameMenufont, new Vector2(-170 + _graphics.PreferredBackBufferWidth / 2, -330 + (_graphics.PreferredBackBufferHeight / 2)), "Press ' Enter ' to start the game ");
        }
        private void DrawGameplay()
        {
           
            DrawTexture(background1Texture, new Vector2(32, 32), 0, new Vector2(32, 32), Vector2.One);
            DrawTexture(moneyCounterTexture, new Vector2(_graphics.PreferredBackBufferWidth / 40, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height / 2), 0, new Vector2(32, 32), Vector2.One);
            DrawTexture(hpCounterTexture, new Vector2(_graphics.PreferredBackBufferWidth / 40, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height * 2), 0, new Vector2(32, 32), Vector2.One);
            DrawTexture(archerbutton, new Vector2(_graphics.PreferredBackBufferWidth - 260, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height / 2), 0, new Vector2(32, 32), Vector2.One);
            DrawTexture(cannonbutton, new Vector2(_graphics.PreferredBackBufferWidth - 160, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height / 2), 0, new Vector2(32, 32), Vector2.One);
            DrawTexture(swordmanbutton, new Vector2(_graphics.PreferredBackBufferWidth - 60, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height / 2), 0, new Vector2(32, 32), Vector2.One);
            DrawText(font, new Vector2(_graphics.PreferredBackBufferWidth / 40, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height + 25), score.ToString());
            DrawText(font, new Vector2((_graphics.PreferredBackBufferWidth / 40) + 8, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height - 76), playerHealth.ToString());
            Drawcursor(customcursor,new Vector2(Mouse.GetState().X, Mouse.GetState().Y),Color.White);
            if (playerHealth <= 0)
            {
                Gameoveraudio();
                
               
                DrawGameover(Gameoverfont, new Vector2(-300 + _graphics.PreferredBackBufferWidth / 2,-50 + (_graphics.PreferredBackBufferHeight / 2)), "GAME OVER");
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
                DrawAnimate(turret.basicTurretTexture, turret.position, turret.rotation, new Vector2(32, 32), Vector2.One);



                if (turret.showUpgrades)
                {
                    DrawTexture(cardBasicTurret2, new Vector2(_graphics.PreferredBackBufferWidth - cardBasicTurret.Width, -cardBasicTurret.Height / 2 + _graphics.PreferredBackBufferHeight / 2), 0f, new Vector2(32, 32), Vector2.One);
                    DrawTexture(turret.shootUppgrade.texture, turret.shootUppgrade.position, turret.shootUppgrade.rotation, turret.shootUppgrade.offset, Vector2.One);
                    DrawTexture(turret.rangeUppgrade.texture, turret.rangeUppgrade.position, turret.rangeUppgrade.rotation, turret.rangeUppgrade.offset, Vector2.One);
                    DrawTexture(rangeTexture, new Vector2(turret.position.X - (turret.rangeTextureScale * rangeTexture.Width /2), turret.position.Y - (turret.rangeTextureScale * rangeTexture.Height/2)), 0, new Vector2(0f, 0f), new Vector2(turret.rangeTextureScale, turret.rangeTextureScale));
                    // Console.WriteLine(turret.rangeTextureScale);
                }
            }
        }
            
       

        #endregion
        private void UpdateGameplay()
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.A) == true)
            {
                mCurrentScreen = ScreenState.Title;
            }

        }
        private void UpdateTitle()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                soundEffects[0].CreateInstance().Play();
                MediaPlayer.Play(ingame);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
            {
                
                mCurrentScreen = ScreenState.Gameplay;
                
            }

        }
        private void Gameoveraudio()
        {
            state += 1;
            if(state == 1)
            {
                MediaPlayer.Stop();
                soundEffects[1].CreateInstance().Play();
            }
            

            
        }
    }
}
