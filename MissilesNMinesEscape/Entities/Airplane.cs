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
using MissilesNMinesEscape.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MissilesNMinesEscape.Entities
{
    /// <summary>
    /// Airplane Class that allows the creation of a Airplane and handles all Airplane logic
    /// </summary>
    public class Airplane : DrawableGameComponent
    {

        public SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position;
        private int delay;

        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delayCounter;

        private const int ROWS = 1;
        private const int COLS = 8;
        private const int HITBOXSHRINK = 20;

        private Vector2 xSpeed;
        private Vector2 ySpeed;
        private Vector2 stage;


        public Vector2 Position { get; set; }

        /// <summary>
        /// Constructor for Airplane, allows for a somewhat customizable Airplane
        /// </summary>
        /// <param name="game">game object</param>
        /// <param name="sb">spritebatch for airplane object</param>
        /// <param name="tex">texture2D for airplane object</param>
        /// <param name="position">airplane position</param>
        /// <param name="xSpeed">airpalne x speed</param>
        /// <param name="ySpeed">airplane y speed</param>
        /// <param name="stage">game form stage</param>
        /// <param name="delay">delay of some animaition</param>
        public Airplane(Game game, SpriteBatch sb, Texture2D tex, Vector2 position, Vector2 xSpeed, Vector2 ySpeed, Vector2 stage, int delay) : base(game)
        {
            this.sb = sb;
            this.tex = tex;
            this.position = position;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.stage = stage;
            this.delay = delay;

            this.dimension = new Vector2(tex.Width / COLS, tex.Height);

            CreateFrames();
        }

        /// <summary>
        /// Method that preps the spritelocation for the Airplane
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
        /// Handles any of the updating and animation and 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                // 만약에 이게 인덱스를 넘어가면 -1로 만들고 안되게하라
                if (frameIndex > ROWS * COLS - 1)
                {
                    frameIndex = 0;
                }


                delayCounter = 0;
            }

            if (ks.IsKeyDown(Keys.Left))
            {
                position -= xSpeed;
                if (position.X < 0)
                {
                    position.X = 0;
                }
                if (position.X > stage.X - frames[frameIndex].Width)
                {
                    position.X = stage.X - frames[frameIndex].Width;
                }
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                position += xSpeed;
                if (position.X > stage.X - frames[frameIndex].Width)
                {
                    position.X = stage.X - frames[frameIndex].Width;
                }
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                position -= ySpeed;
                if (position.Y < 0)
                {
                    position.Y = 0;
                }

                if (position.Y > stage.Y - frames[frameIndex].Height)
                {
                    position.Y = stage.Y - frames[frameIndex].Height;
                }
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                position += ySpeed;
                if (position.Y > stage.Y - frames[frameIndex].Height)
                {
                    position.Y = stage.Y - frames[frameIndex].Height;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the bullet into frame with all the class variables
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {

            if (frameIndex >= 0)
            {
                sb.Begin();
                sb.Draw(tex, position, frames[frameIndex], Color.White);
                sb.End();
            }
            base.Draw(gameTime);
        }

        /// <summary>
        /// Method to get the boundry / hitbox of the bullet
        /// </summary>
        /// <returns>Rectangle</returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y - HITBOXSHRINK * 2, (int)dimension.X - HITBOXSHRINK * 2, (int)dimension.Y);
        }
    }
}