using MissilesNMinesEscape.Entities;
using MissilesNMinesEscape.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissilesNMinesEscape.Scenes
{
    /// <summary>
    /// Highscore scene to display the users highscore after game end
    /// </summary>
    public class HighScoreScene : GameScene
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont headerFont;
        private SpriteFont contentFont;
        private List<PlayerInfo> highScores;
        SavingScoreManager savingScoreManager = new SavingScoreManager();

        /// <summary>
        /// Constructor for highscore
        /// </summary>
        /// <param name="game"></param>
        public HighScoreScene(Game game) : base(game)
        {
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
            headerFont = game.Content.Load<SpriteFont>("fonts/GameModeSelectedFont");
            contentFont = game.Content.Load<SpriteFont>("fonts/ContentFont");

        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadAndDisplayHighScores()
        {
            // Load high scores using SavingScoreManager
            highScores = SavingScoreManager.LoadTop5HighScores();
            DisplayHighScores();
        }
        /// <summary>
        /// Method to display the highscores
        /// </summary>
        private void DisplayHighScores()
        {
            _spriteBatch.Begin();

            // Display header
            _spriteBatch.DrawString(headerFont, "High Scores", new Vector2(10, 10), Color.Black);

            // Display each high score entry
            for (int i = 0; i < highScores.Count; i++)
            {
                string playerInfo = $"TOP{i + 1}. Name: {highScores[i].PlayerName}   Score: {highScores[i].PlayerScore}  Mode: {highScores[i].GameMode}";
                _spriteBatch.DrawString(contentFont, playerInfo, new Vector2(130, 110 + i * 70), Color.Navy);
            }

            _spriteBatch.End();
        }
        /// <summary>
        /// Draws the highScore to the user
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            LoadAndDisplayHighScores();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Updates the screen
        /// </summary>
        /// <param name="gameTime"></param>

        public override void Update(GameTime gameTime)
        {
            LoadAndDisplayHighScores();
            base.Update(gameTime);
        }
    }
}
