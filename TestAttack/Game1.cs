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
        public List<Player> players = new List<Player>(5);
        public int timer = 60;
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
          
            for (int i = 0; i < 5; i++)
            {
                players.Add(new Player(texture3));
               
            }
            foreach (var player in players)
            {
                Random r = new Random();
                player.Position = new Vector2(r.Next(0, 800), r.Next(0, 600));
            }

            _player = new Player(texture) { Position = new Vector2(100, 100), speed = 10 };
           // _player2 = new Player(texture) { Position = new Vector2(100, 100), speed = 10 };
            _bullet = new Bullet(texture2) { Position = new Vector2(300, 100), };

           /* _players = new List<Player>()
            {
                
                new Player(texture) {Position = new Vector2(200, 100), speed = 5}
            };*/
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _player.Move();      
            // foreach (var player in _players)
            //player.Move();     
             _bullet.Shoot(players[_bullet.Counter]);
            Checktimer();
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _player.Draw(_spriteBatch);
            
            _bullet.Draw(_spriteBatch);

            foreach (var player in players)
                player.Draw(_spriteBatch);

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
                
                    players[_bullet.Counter-1].Position = new Vector2(-100, -100);
                
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