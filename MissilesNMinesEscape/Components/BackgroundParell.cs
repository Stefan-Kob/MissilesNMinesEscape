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
    /// Class to support the background of the game
    /// </summary>
    public class BackgroundParell : DrawableGameComponent
    {


        private SpriteBatch sb;
        private Texture2D tex;

        private Vector2 position1, position2;
        private Rectangle srcRect;
        private Vector2 speed;

        /// <summary>
        /// Constructor to make the background
        /// </summary>
        /// <param name="game">game object</param>
        /// <param name="sb">sprite batch for background</param>
        /// <param name="tex">texture 2d for background</param>
        /// <param name="position">background position</param>
        /// <param name="srcRect">background ooriginal size</param>
        /// <param name="speed">background speed </param>
        public BackgroundParell(Game game, SpriteBatch sb, Texture2D tex, Vector2 position, Rectangle srcRect, Vector2 speed) : base(game)
        {
            this.sb = sb;
            this.tex = tex;
            this.srcRect = srcRect;

            this.position1 = position;
            this.position2 = new Vector2(position1.X + srcRect.Width, position1.Y);
            this.speed = speed;

        }

        /// <summary>
        /// Updates the speed and position of the animation
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            position1 -= speed;
            position2 -= speed;

            if (position1.X < -srcRect.Width)
            {
                position1.X = position2.X + srcRect.Width;
            }

            if (position2.X < -srcRect.Width)
            {
                position2.X = position1.X + srcRect.Width;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the background to the user
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();

            sb.Draw(tex, position1, srcRect, Color.White);
            sb.Draw(tex, position2, srcRect, Color.White);

            sb.End();
            base.Draw(gameTime);
        }
    }
}
