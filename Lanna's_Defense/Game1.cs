using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
// using System.Formats.Asn1;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.Collections.Generic;

namespace Lanna_s_Defense
{
    public class Game1 : Game
    {
        List<String> path1 = new List<string>() {"r3.45", "u3.7", "r7.2", "d7.3", "l3.6", "u1.4", "l2.25", "u4.5","r4.5", "d2.15", "l2.275", "s"};
        List<String> path1_heavy = new List<string>() { "r3.45", "u3.7", "r7.2", "d7.3", "l3.6", "u1.4", "l2.25", "u4.5", "r4.5", "d2.15", "l2.275", "s" };
        List<String> path1_fast = new List<string>() { "r3.45", "u3.7", "r7.2", "d7.3", "l3.6", "u1.4", "l2.25", "u4.5", "r4.5", "d2.15", "l2.275", "s" };
        static List<Enemy> enemyList = new List<Enemy>();
        static List<Turret> turretList = new List<Turret>();


        double gt = 0;
        static double staticGt = 0;
        double timeSinceLast = 0;
        int enemiesKilled = 0;

        private SpriteFont font;
        public static int score = 2000;
        Texture2D backgroundPath1Texture;
        Texture2D background1Texture;
        
        // Texture2D monster1Texture;
        static public Texture2D monster2Idle;
        static public Texture2D monster3Idle;
        static public Texture2D monster1Idle;
        static public Texture2D monsterHit;

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
            _graphics.PreferredBackBufferWidth = 750;  
            _graphics.PreferredBackBufferHeight = 750;   
            _graphics.ApplyChanges();
        }
        protected override void LoadContent()
        {
            

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Gamefont");
            background1Texture = Content.Load<Texture2D>("map4");
            //backgroundPath1Texture = Content.Load<Texture2D>("EnemyPath2");
            
            cardBasicTurret = Content.Load<Texture2D>("CardBasicturret");
            shootSpeedUpgrade = Content.Load<Texture2D>("ShootSpeedUpgrade");
            rangeUpgrade = Content.Load<Texture2D>("RangeUpgrade");

            monster1Idle = Content.Load<Texture2D>("lf farmer");
            monster2Idle = Content.Load<Texture2D>("Ball");
            monster3Idle = Content.Load<Texture2D>("Rockman_walk");
            monsterHit = Content.Load<Texture2D>("monsterHit");

            turretBaseTexture = Content.Load<Texture2D>("Base");
            moneyCounterTexture = Content.Load<Texture2D>("MoneyCounter");
            rangeTexture = Content.Load<Texture2D>("RangeTexture");

            basicTurretIdle = Content.Load<Texture2D>("BasicTurret");
            basicTurretShoot = Content.Load<Texture2D>("BasicTurretShoot");
            
            whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });

            mouseController = new MouseController(turretList, turretBaseTexture.Width, turretBaseTexture.Height, gt);

            this.IsMouseVisible = true;
        }
        public static Texture2D changeMonster1Texture(Texture2D current, string type){
            if(current == monster1Idle || current == monster2Idle || current == monster3Idle){
                return monsterHit;
            }else{
                switch (type)
                {
                    case "normal":
                        return monster1Idle;
                    case "heavy": 
                        return monster2Idle;
                    case "fast":
                        return monster3Idle;
                    default:
                        return monster1Idle;
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


        void WaveManager(){
            if (gt > timeSinceLast + spawnRate)
            {
                if(enemyList.Count + enemiesKilled < spawnAmount){
                    AddEnemy();
                    timeSinceLast = gt;
                }else{
                    NextWave();
                }
            }
        }
        void NextWave(){
            if (enemiesKilled == spawnAmount)
            {
                Console.WriteLine("Next Wave!");
                spawnAmount += 10;
                if(higherMonsterChance != -40){
                    higherMonsterChance -= 20;
                }
                enemiesKilled = 0;
                if(spawnRate >= 1000){
                    spawnRate -= 500;
                }
                timeSinceLast = gt;
            }
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseController.MouseUpdate();

            mouseController.gt = gt;


            WaveManager();

            gt = gameTime.TotalGameTime.TotalMilliseconds;
            staticGt = gt;
            
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

                    
                }
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i].Health <= 0)
                    {
                        EnemyDie(i);
                        break;
                    }
                    if (enemyList[i].Position.X > _graphics.PreferredBackBufferWidth + enemyList[i].monsterTexture.Width)
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
            Vector2 pos = new Vector2(-64 / 2, (_graphics.PreferredBackBufferHeight / 2)+40);
            int num = getRandomNum();
            if(num >= 55 - higherMonsterChance){
                Enemy enemy = new Enemy(path1, pos, 60f, gt, 35, monster1Idle, "normal");
                enemyList.Add(enemy);
                enemy.Start();
            }
            else if (num >= 34 - higherMonsterChance)
            {
                Enemy enemy = new Enemy(path1_heavy, pos, 60f, gt, 300, monster2Idle, "heavy");
                enemyList.Add(enemy);
                enemy.Start();
            }
            else
            {
                Enemy enemy = new Enemy(path1_fast, pos, 120f, gt, 35, monster3Idle, "fast");
                enemyList.Add(enemy);
                enemy.Start();
            }
        
        }
        int getRandomNum(){
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
                Turret turret = new Turret(position, enemyList, 275f, staticGt, 60, basicTurretIdle, shootUppgradeCard, rangeUppgradeCard);

                turretList.Add(turret);
            }

        }
        public static void DeleteTurret(int turretIndex)
        {
            turretList.RemoveAt(turretIndex);
            score += 200;
        }



        void EnemyDie(int index){
            enemiesKilled++;
            score += 50;
            enemyList.RemoveAt(index);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawTexture(background1Texture, new Vector2(32, 32), 0, new Vector2(32, 32), Vector2.One);
            DrawTexture(moneyCounterTexture, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height / 2), 0, new Vector2(32, 32), Vector2.One);
            DrawText(font, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height + 25), score.ToString());
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
                DrawTexture(enemy.monsterTexture, enemy.Position, 0, new Vector2(32, 64), Vector2.One);
                
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
                Color.Black
            );
            _spriteBatch.End();
        }
    }
}
