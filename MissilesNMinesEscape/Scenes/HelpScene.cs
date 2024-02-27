using MissilesNMinesEscape.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace MissilesNMinesEscape.Scenes
{
    /// <summary>
    /// Help scene to display help and game functions
    /// </summary>
    public class HelpScene : GameScene
    {
        private Texture2D image;
        private SpriteBatch _spriteBatch;
        private Game g;

        /// HelpScene constructor
        public HelpScene(Game game) : base(game)
        {
            g = game;
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            image = game.Content.Load<Texture2D>("images/help");

        }

        /// <summary>
        /// Checks to see if the user wants to exit the help scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                g.Exit();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the help screen to the user
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(image, new Vector2(0, 0), Color.White);
            _spriteBatch.End();
        }
    }
}
