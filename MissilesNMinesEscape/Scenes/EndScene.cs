using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using MissilesNMinesEscape.Entities;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MissilesNMinesEscape.Manager;
using SharpDX.DirectWrite;
using FinalPlayerNameInput;

namespace MissilesNMinesEscape.Scenes
{
    /// <summary>
    /// EndScene class that is used when the player etheir wins or dies. Dislpays the users game stats and allows replay
    /// </summary>
    public class EndScene : GameScene
    {
        private static bool isFormShown = false;
        private static bool isSubmitName = false;
        private Form1 finalPlayerNameInput;
        private SpriteBatch _spriteBatch;
        private SpriteFont gameOverFont;
        private SpriteFont normalFont;
        StartScene startScene;

        private PlayerInfo playerInfo;
        private int finalScore;
        private int timeScore = 0;
        private int coinScore = 0;
        private int bonusScore = 0;

        private Texture2D kaboomTex;
        private SoundEffect kaboomSound;
        private const int KABOOMMOVESIZE = 50;
        SavingScoreManager savingScoreManager = new SavingScoreManager();

        private Rectangle retryRect;
        private Rectangle exitRect;
        private string bonusMsg = string.Empty;
        private bool passed;
        Game1 g;

        /// <summary>
        /// Constructor to build the end scene. Notice passed for indicating if they acually won the level or not
        /// </summary>
        /// <param name="game">game object</param>
        /// <param name="timeScore">time score from gameScene</param>
        /// <param name="coinScore">coin score from gameScene</param>
        /// <param name="mode">selected mode</param>
        /// <param name="location">location for airplane</param>
        /// <param name="passed">whether type name or not</param>
        public EndScene(Game game, int timeScore, int coinScore, string mode, Vector2 location, bool passed) : base(game)
        {
            playerInfo = new PlayerInfo();
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
            gameOverFont = game.Content.Load<SpriteFont>("fonts/GameOverFont");
            normalFont = game.Content.Load<SpriteFont>("fonts/GameModeNormalFont");
            this.timeScore = timeScore;
            this.coinScore = coinScore;
            playerInfo.GameMode = mode;
            kaboomTex = game.Content.Load<Texture2D>("images/kaboom");
            kaboomSound = game.Content.Load<SoundEffect>("audios/kaboomSound");
            g = (Game1)game;
            this.passed = passed;
            startScene = g.StartScene;


            if (passed == false)
            {
                kaboomSound.Play();
            }
            else
            {
                bonusMsg = "+200 Bonus!";
                bonusScore = 200;
            }

            finalScore = timeScore + coinScore + bonusScore;
            retryRect = new Rectangle(300, 410, 100, 50);
            exitRect = new Rectangle(460, 410, 100, 50);
            hide();
        }

        /// <summary>
        /// Draws the endScene to the user
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(gameOverFont, $"Game Over", new Vector2(170, 75), Color.OrangeRed);
            _spriteBatch.DrawString(normalFont, $"Survival Score      : {timeScore}\n\nCoins Collected     : {coinScore}\n\nFinal Score            : {finalScore} {bonusMsg}", new Vector2(220, 200), Color.Black);
            _spriteBatch.DrawString(normalFont, "Retry", new Vector2(retryRect.X, retryRect.Y), Color.Orange);
            _spriteBatch.DrawString(normalFont, "Exit", new Vector2(exitRect.X, exitRect.Y), Color.Orange);

            _spriteBatch.End();

        }

        /// <summary>
        /// If the endscene is enabled, call the handle input class
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (this.Enabled)
            {
                HandleInput();
            }
        }

        /// <summary>
        /// Method for compliling player info
        /// </summary>
        private void SavePlayerInfo()
        {

            PlayerInfo currentPlayerInfo = new PlayerInfo
            {
                PlayerName = playerInfo.PlayerName,
                PlayerScore = finalScore,
                GameMode = playerInfo.GameMode
            };

            savingScoreManager.MakingFile();
            savingScoreManager.AddNewPlayerInfo(currentPlayerInfo);
        }

        /// <summary>
        /// Method that hadles the user input to save a score
        /// </summary>
        private void HandleInput()
        {
            KeyboardState ks = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (!isFormShown)
            {
                finalPlayerNameInput = new Form1();
                isFormShown = true;

                do
                {
                    finalPlayerNameInput.ShowDialog();
                    string tempName = finalPlayerNameInput.UserName;
                    if (tempName != null)
                    {
                        playerInfo.PlayerName = tempName;
                        isSubmitName = true;
                        break;
                    }
                } while (true);


            }
            if (retryRect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && isSubmitName == true)
            {
                PlayMusic();
                Game.Components.Remove(this);
                g.PlayMenuScene.show();
                isFormShown = false;
                isSubmitName = false;
                SavePlayerInfo();
            }

            if (exitRect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && isSubmitName == true)
            {
                PlayMusic();
                Game.Components.Remove(this);
                startScene.show();
                isFormShown = false;
                isSubmitName = false;
                SavePlayerInfo();
            }

            if (ks.IsKeyDown(Keys.Escape) && isSubmitName == true)
            {
                Game.Components.Remove(this);
                startScene.show();
                isFormShown = false;
                isSubmitName = false;
                SavePlayerInfo();
            }
        }
        /// <summary>
        /// Method to play music, here to reduce redundancy
        /// </summary>
        private void PlayMusic()
        {
            Song backgroundMusic = g.Content.Load<Song>("audios/Nio");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
        }
    }
}
