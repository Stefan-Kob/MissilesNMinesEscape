using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissilesNMinesEscape.Entities
{
    /// <summary>
    /// Kaboom class that handles any explosions on screen if used
    /// </summary>
    public class Kaboom : DrawableGameComponent
    {
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position;
        private int delay;

        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;

        private int delayCounter;

        private const int ROWS = 4;
        private const int COLS = 4;

        public Vector2 Position { get => position; set => position = value; }

        private Game g;

        /// <summary>
        /// Constructor for kaboom, allows for a somewhat customizable explosion(kaboom) 
        /// </summary>
        /// <param name="game">game object</param>
        /// <param name="sb">spritebatch for kabom object</param>
        /// <param name="tex">texture2D for kabom object</param>
        /// <param name="position">kabom position</param>
        /// <param name="delay">kabom delay for animation<param>
        public Kaboom(Game game, SpriteBatch sb, Texture2D tex, Vector2 position, int delay) : base(game)
        {
            this.g = game;
            this.sb = sb;
            this.tex = tex;
            this.Position = position;
            this.delay = delay;
            this.dimension = new Vector2(tex.Width / COLS, tex.Height / ROWS);
            CreateFrames();
            Hide();
        }

        /// <summary>
        /// Method that preps the spritelocation for the missile
        /// </summary>
        private void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }

        /// <summary>
        /// Hides kaboom
        /// </summary>
        public void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        /// <summary>
        /// Shows kaboom
        /// </summary>
        public void Show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        /// <summary>
        /// Handles any of the updating and animation
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > ROWS * COLS - 1)
                {
                    frameIndex = -1;
                    Hide();
                    g.Components.Remove(this);
                }

                delayCounter = 0;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the kaboom into frame
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            if (frameIndex >= 0)
            {
                sb.Begin();
                // Version 4 of the draw function
                sb.Draw(tex, Position, frames[frameIndex], Color.White);
                sb.End();

            }

            base.Draw(gameTime);
        }

    }
}
