﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MissilesNMinesEscape.Entities
{
    /*
     *Missile.cs
     *Missile Class that allows the creation of a missle and handles all missile logic
     *
     *Revision History : Finished
     *Kihoon/Stefan, 2023/12/4: Created
     */
    public class Missile : DrawableGameComponent
    {
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position;
        private int delay;
        private int speed;

        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;

        private int delayCounter;

        private const int ROWS = 3;
        private const int COLS = 1;
        private const int HITBOXSHRINK = 20;

        public Vector2 Position { get => position; set => position = value; }
        private Game g;

        /// <summary>
        /// Constructor for missile, allows for a somewhat customizable missile
        /// </summary>
        /// <param name="game">game object</param>
        /// <param name="sb">spritebatch for misilie object</param>
        /// <param name="tex">texture2D for misilie object</param>
        /// <param name="position">misilie position</param>
        /// <param name="delay">misilie delay for animation<param>
        /// <param name="speed">misilie speed</param>
        public Missile(Game game, SpriteBatch sb, Texture2D tex, Vector2 position, int delay, int speed) : base(game)
        {
            this.g = game;
            this.sb = sb;
            this.tex = tex;
            this.Position = position;
            this.delay = delay;
            this.speed = speed;
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
        /// Handles any of the updating and animation
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            position += new Vector2(speed, 0);

            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex == ROWS * COLS)
                {
                    frameIndex = 1;
                }
                if (frameIndex > ROWS * COLS - 1)
                {
                    frameIndex = -1;
                }

                delayCounter = 0;
            }

            if (position.X <= -100)
            {
                Hide();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the missile into frame with all the class variables
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            if (frameIndex >= 0)
            {
                sb.Begin();
                sb.Draw(tex, Position, frames[frameIndex], Color.White);
                sb.End();

            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// Hides the missile
        /// </summary>
        public void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        /// <summary>
        /// Shows the missile
        /// </summary>
        public void Show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        /// <summary>
        /// Method to get the boundry / hitbox of the missile
        /// </summary>
        /// <returns></returns>
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y - HITBOXSHRINK * 2, (int)dimension.X - HITBOXSHRINK * 2, (int)dimension.Y);
        }
    }
}
