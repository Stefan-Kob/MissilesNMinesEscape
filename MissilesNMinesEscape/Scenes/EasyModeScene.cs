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
    public class EasyModeScene : GameScene
    {
        private Game1 g;
        private CollisionManager _collisionManager;
        private SpriteBatch _spriteBatch;
        private SpriteFont spriteFont;
        private Texture2D missileTex;
        private Texture2D groundBombTex;
        private Texture2D airBombTex;
        private Texture2D coinTex;
        private Missile missile;
        private MineBomb mineBomb;
        private Coin coin;
        private Airplane airplane;
        private SoundEffect coinCollectSound;
        private List<Missile> missileList;
        private List<MineBomb> mineBombList;
        private List<Coin> coinList;

        private int coinListCount = 0;
        private int coinSoundCount = 0;
        private int timeScore = 0;
        private int coinScore = 0;

        private int coinPos = 800;
        private int airMinePos = 800;
        private int groundMinePos = 700;
        private int missilePos = 1500;
        private int airMineCount = 24;
        private int groundMineCount = 20;
        private int missileCount = 15;
        private const int coinAmount = 30;
        private float scoreUpdateInterval = 0.05f;
        private float scoreTimer = 0.0f;

        StartScene startScene;

        public bool isGameDone = false;
        public bool passed = false;

        /// <summary>
        /// Constructor to load the game materials
        /// </summary>
        /// <param name="game"></param>
        public EasyModeScene(Game game) : base(game)
        {
            g = (Game1)game;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            coinCollectSound = g.Content.Load<SoundEffect>("audios/coinCollect");
            KeyboardState ks = Keyboard.GetState();
            Vector2 stage = new Vector2(SharingComponent.stage.X, SharingComponent.stage.Y);

            startScene = g.StartScene;

            Texture2D airplaneTex = game.Content.Load<Texture2D>("images/Airplane");
            Vector2 airplaneInitPos = new Vector2(70, 200);
            Vector2 airplaneXSpeed = new Vector2(6, 0);
            Vector2 airplaneYSpeed = new Vector2(0, 6);


            airplane = new Airplane(game, _spriteBatch, airplaneTex, airplaneInitPos, airplaneXSpeed, airplaneYSpeed, stage, 3);
            this.Components.Add(airplane);

            // Addition of Missile
            for (int i = 0; i < missileCount; i++)
            {
                int randomPosAway = RandomNumberGenerator.GetInt32(425, 800);
                int randomPosHigh = RandomNumberGenerator.GetInt32(25, 350);
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
                int randomPosAway = RandomNumberGenerator.GetInt32(250, 400);
                int randomPosHigh = RandomNumberGenerator.GetInt32(5, 180);

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
                int randomPosAway = RandomNumberGenerator.GetInt32(325, 450);
                int randomPosHigh = RandomNumberGenerator.GetInt32(210, 345);

                groundMinePos = groundMinePos + randomPosAway;
                groundBombTex = game.Content.Load<Texture2D>("images/mineBombGroundHigh");
                mineBomb = new MineBomb(game, _spriteBatch, groundBombTex, new Vector2(groundMinePos, randomPosHigh), 10);
                this.Components.Add(mineBomb);
                mineBomb.Show();
            }

            missileList = this.Components.OfType<Missile>().ToList();
            mineBombList = this.Components.OfType<MineBomb>().ToList();
            coinList = this.Components.OfType<Coin>().ToList();

            _collisionManager = new CollisionManager(g, missileList, mineBombList, coinList, mineBomb, airplane, this); // 수정된 부분
            this.Components.Add(_collisionManager);
        }

        /// <summary>
        /// Controls what makes the game objects appear
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            float timeScore = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateScore(timeScore);
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
            if (timeScore == 1500)
            {
                passed = true;
                EndGame();
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

            base.Update(gameTime);
        }

        /// <summary>
        /// Method to update the users game score
        /// </summary>
        /// <param name="elapsedSeconds">the parameter of score </param>
        private void UpdateScore(float score)
        {
            scoreTimer += score;

            if (scoreTimer >= scoreUpdateInterval)
            {
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
                    coinListCount += 1;
                }
            }
            coinScore = coinListCount;
            Rectangle airplaneBox = airplane.getBounds();
            int x = airplaneBox.X;
            int y = airplaneBox.Y;
            isGameDone = true;

            EndScene endScene = new EndScene(g, timeScore, coinScore, "Easy Mode", new Vector2(x, y), passed);
            Game.Components.Add(endScene);
            endScene.show();
            Game.Components.Remove(this);
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