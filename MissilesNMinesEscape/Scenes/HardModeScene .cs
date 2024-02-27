/*
 * Programmed by: Kihoon Kim and Stefan Kobetich
 * Game Name: MissilesNMinesEscape 
 * Revision history:
 *          01-DEC 2023: Project created
 *          02-DEC 2023: Designed game
 *          03-DEC 2023: Make some entity
 *          03-DEC 2023: Complete collision
 *          05-DEC 2023: Debugging process
 *          06-DEC 2023: Organize scenes
 *          08-DEC 2023: Debugging complete
 *          09-DEC 2023: Project complete        
 */
using MissilesNMinesEscape.Entities;
using MissilesNMinesEscape.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace MissilesNMinesEscape.Scenes
{
    /// <summary>
    /// Easy gamemode game class: The class that houses the easy version of the game
    /// </summary>
    public class HardModeScene : GameScene
    {
        private Game1 g;
        private CollisionManager _collisionManager;
       
        private SpriteBatch _spriteBatch;
        private SpriteFont spriteFont;
        
        private Texture2D missileTex;
        private Texture2D groundBombTex;
        private Texture2D airBombTex;
        private Texture2D coinTex;
        private Texture2D heliTex;
        
        private Texture2D bulletTex;
        private Missile missile;
        private Helli helli;
        private MineBomb mineBomb;
        private Coin coin;
        private Bullet bullet;   
        private Airplane airplane;
        private SoundEffect coinCollectSound;
        
        private List<Missile> missileList;
        private List<MineBomb> mineBombList;
        private List<Coin> coinList;
        private List<Helli> helliList;
        private List<Bullet> bulletList;

        private StartScene startScene;

        private int coinListCount = 0;
        private int coinSoundCount = 0;
        private int timeScore = 0;
        private int bulletCount = 0;
        private int bulletTimeDelay = 150;
        private int coinScore = 0;
        private int coinPos = 800;
        private int airMinePos = 650;
        private int loopCount = 0;
        private int groundMinePos = 650;
        private int missilePos = 1150;
        private int airMineCount = 24;
        private int groundMineCount = 35;
        private int missileCount = 27;
        private const int coinAmount = 50;
        private float scoreUpdateInterval = 0.05f;
        private float scoreTimer = 0.0f;

        private bool passed = false;
        public bool isGameDone = false;


        /// <summary>
        /// Constructor to load the game materials
        /// </summary>
        /// <param name="game"></param>
        public HardModeScene(Game game) : base(game)
        {
            g = (Game1)game;

            startScene = g.StartScene;

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            coinCollectSound = g.Content.Load<SoundEffect>("audios/coinCollect");
            KeyboardState ks = Keyboard.GetState();
            Vector2 stage = new Vector2(SharingComponent.stage.X, SharingComponent.stage.Y);
            heliTex = g.Content.Load<Texture2D>("images/heli");
            bulletTex = g.Content.Load<Texture2D>("images/bullet");

            // Addition of airplane
            //Texture2D airplaneTex = game.Content.Load<Texture2D>("images/AirPlane1");
            Texture2D airplaneTex = game.Content.Load<Texture2D>("images/Airplane");
            Vector2 airplaneInitPos = new Vector2(200, 200);
            Vector2 airplaneXSpeed = new Vector2(6, 0);
            Vector2 airplaneYSpeed = new Vector2(0, 6);

            //airplane = new Airplane(game, _spriteBatch, airplaneTex, airplaneInitPos, airplaneXSpeed, airplaneYSpeed, stage);
            //this.Components.Add(airplane);
            airplane = new Airplane(game, _spriteBatch, airplaneTex, airplaneInitPos, airplaneXSpeed, airplaneYSpeed, stage, 3);
            this.Components.Add(airplane);

            // Addition of Missile
            for (int i = 0; i < missileCount; i++)
            {
                int randomPosAway = RandomNumberGenerator.GetInt32(375, 700);
                int randomPosHigh = RandomNumberGenerator.GetInt32(20, 400);
                int randomSpeed = RandomNumberGenerator.GetInt32(-5, -2);

                missilePos = missilePos + randomPosAway;
                if (randomSpeed == -3 || randomSpeed == -2)
                {
                    missileTex = game.Content.Load<Texture2D>("images/smallMissile");
                }
                else
                {
                    missileTex = game.Content.Load<Texture2D>("images/MissileFire");
                }
                missile = new Missile(game, _spriteBatch, missileTex, new Vector2(missilePos, randomPosHigh), 5, randomSpeed);
                this.Components.Add(missile);
                missile.Show();
            }

            // Addition of MineBomb air
            for (int i = 0; i < airMineCount; i++)
            {
                int randomPosAway = RandomNumberGenerator.GetInt32(250, 375);
                int randomPosHigh = RandomNumberGenerator.GetInt32(5, 200);

                airMinePos = airMinePos + randomPosAway;
                airBombTex = game.Content.Load<Texture2D>("images/floatingMineBomb");
                mineBomb = new MineBomb(game, _spriteBatch, airBombTex, new Vector2(airMinePos, randomPosHigh), 10);
                this.Components.Add(mineBomb);
                mineBomb.Show();
            }

            // Addition of coins
            for (int i = 0; i < coinAmount; i++)
            {
                int randomPosAway = RandomNumberGenerator.GetInt32(50, 400);
                int randomPosHigh = RandomNumberGenerator.GetInt32(50, 150);

                coinPos = coinPos + randomPosAway;
                coinTex = game.Content.Load<Texture2D>("images/coin");
                coin = new Coin(game, _spriteBatch, coinTex, new Vector2(coinPos, randomPosHigh), 8);
                this.Components.Add(coin);
                coin.Show();
            }

            // Addition of MineBomb ground
            for (int i = 0; i < groundMineCount; i++)
            {
                int randomPosAway = RandomNumberGenerator.GetInt32(275, 400);
                int randomPosHigh = RandomNumberGenerator.GetInt32(210, 345);

                groundMinePos = groundMinePos + randomPosAway;
                groundBombTex = game.Content.Load<Texture2D>("images/mineBombGroundHigh");
                mineBomb = new MineBomb(game, _spriteBatch, groundBombTex, new Vector2(groundMinePos, randomPosHigh), 10);
                this.Components.Add(mineBomb);
                mineBomb.Show();
            }

        }

        /// <summary>
        /// Controls what makes the game objects appear
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            float elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateScore(elapsedSeconds);
            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                startScene.show();
                Game.Components.Remove(this);
            }
            if (isGameDone)
            {
                EndGame();
                return;
            }
            if (timeScore == 2500)
            {
                passed = true;
                EndGame();
            }
            if (timeScore == 125)
            {
                helli = new Helli(g, _spriteBatch, heliTex, new Vector2(45, 75), 5);
                this.Components.Add(helli);
                helli.Show();
            }
            if (bulletCount == 20)
            {
                helli.Hide();
            }
            else
            {
                if (timeScore == bulletTimeDelay)
                {
                    if (loopCount == 0)
                    {
                        AddBullet();
                    }
                }
                if (timeScore == bulletTimeDelay + 1)
                {
                    int randomDelay = RandomNumberGenerator.GetInt32(25, 100);
                    loopCount = 0;
                    bulletTimeDelay += randomDelay;
                }
            }


            coinList = this.Components.OfType<Coin>().ToList();
            foreach (Coin coin in coinList)
            {
                if (coin.Enabled == false)
                {
                    coinListCount++;
                }
            }
            if (coinListCount != coinSoundCount)
            {
                coinSoundCount++;
                coinCollectSound.Play();
            }
            coinListCount = 0;

            this.Components.Remove(_collisionManager);
            coinList = this.Components.OfType<Coin>().ToList();
            helliList = this.Components.OfType<Helli>().ToList();
            bulletList = this.Components.OfType<Bullet>().ToList();
            missileList = this.Components.OfType<Missile>().ToList();
            mineBombList = this.Components.OfType<MineBomb>().ToList();
            _collisionManager = new CollisionManager(g, missileList, mineBombList, helliList, coinList, bulletList, mineBomb, airplane, this, helli, bullet);
            this.Components.Add(_collisionManager);

            base.Update(gameTime);
        }
        
        /// <summary>
        /// Method that gets used when the helli wants to shoot a bullet
        /// </summary>
        private void AddBullet()
        {
            Rectangle helliRect = helli.getBounds();
            int randomSpeed = RandomNumberGenerator.GetInt32(3, 4);
            int x = helliRect.X;
            int y = helliRect.Y;

            bullet = new Bullet(g, _spriteBatch, bulletTex, new Vector2(x, y), 5, randomSpeed);
            this.Components.Add(bullet);
            bullet.Show();
            bulletCount++;
            loopCount = 1;
        }

        // Method to update the users game score
        private void UpdateScore(float elapsedSeconds)
        {
            scoreTimer += elapsedSeconds;

            if (scoreTimer >= scoreUpdateInterval)
            {
                // 경과 시간 누적
                timeScore += 1;

                // initialize scoreTimertimer
                scoreTimer = 0.0f;

            }
        }

        /// <summary>
        /// Draws the score to the user as they play
        /// </summary>
        private void DrawScore()
        {
            spriteFont = g.Content.Load<SpriteFont>("fonts/GameModeSelectedFont");
            _spriteBatch.Begin();
            _spriteBatch.DrawString(spriteFont, "Score: " + timeScore, new Vector2(10, 10), Color.OrangeRed);
            _spriteBatch.End();
        }
        
        /// <summary>
        /// Method that gets called when the game ends, handles game ending functions
        /// </summary>
        public void EndGame()
        {
            coinList = this.Components.OfType<Coin>().ToList();
            foreach (Coin coin in coinList)
            {
                if (coin.Enabled == false)
                {
                    coinListCount+=2;
                }
            }
            coinScore = coinListCount;
            Rectangle airplaneBox = airplane.getBounds();
            int x = airplaneBox.X;
            int y = airplaneBox.Y;
            isGameDone = true;

            EndScene endScene = new EndScene(g, timeScore, coinScore, "Hard Mode", new Vector2(x, y), passed);
            g.Components.Add(endScene);
            endScene.show();
            g.Components.Remove(this);
        }

        /// <summary>
        /// Allows the game to be drawn to the user
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            DrawScore();
            base.Draw(gameTime);
        }
    }
}