using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Threading;
using TestBullet;

namespace TestAttack
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Player _player,_player2;
        Bullet _bullet;
        public bool Ishit;
        public List<Enemy> Enemies = new List<Enemy>(5);
        public int timer = 60;
        public static int maxPlayer =5;
       // private List<Player> _players;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var texture = Content.Load<Texture2D>("char1");
            var texture3 = Content.Load<Texture2D>("osu2");
            var texture2 = Content.Load<Texture2D>("bullet3");
          
            for (int i = 1; i <= maxPlayer; i++)
            {
                Enemies.Add(new Enemy(texture3));
               
            }
            foreach (var Enemy in Enemies)
            {
                Random r = new Random();
                Enemy.Position = new Vector2(r.Next(0, 800), r.Next(0, 600));
            }

            _player = new Player(texture) { Position = new Vector2(100, 100), speed = 10 };
            _bullet = new Bullet(texture2) { Position = new Vector2(300, 100), };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _player.Move();      
   
             _bullet.Shoot(Enemies[_bullet.Counter]);
            Checktimer();
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _player.Draw(_spriteBatch);
            _bullet.Draw(_spriteBatch);
            foreach (var Enemy in Enemies)
                Enemy.Draw(_spriteBatch);

           // foreach (var player in _players)
             //   player.Draw(_spriteBatch);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void Checktimer()
        {
            if (_bullet.Ishit == true)
            {
                
                    Enemies[_bullet.Counter-1].Position = new Vector2(-100, -100);
                
                timer--;
                Console.WriteLine(timer);
                _bullet.Position = new Vector2(0, 0);
                if (timer <= 0)
                {
                    _bullet.Ishit = false;
                    timer = 60;
                }
            }
        }
    }
}