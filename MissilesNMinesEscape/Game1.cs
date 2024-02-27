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
using MissilesNMinesEscape.Scenes;
using MissilesNMinesEscape.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Linq;

namespace MissilesNMinesEscape
{
    
    /// <summary>
    /// Class that handles the main menu logic. Allows navigation to the other scenes
    /// </summary>
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        // Decare all scenes
        private StartScene startScene;
        private HelpScene helpScene;
        private PlayMenuScene playMenuScene;
        private CreditsScene creditsScene;
        private HighScoreScene highScoreScene;


        public StartScene StartScene { get => startScene; set => startScene = value; }
        public PlayMenuScene PlayMenuScene { get => playMenuScene; set => playMenuScene = value; }

        /// <summary>
        /// Default constructor to initialize some game comonents
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initilizes the stage
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // stage가 static여서 이렇게 불러오고 preffered로 하면된다.

            SharingComponent.stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        /// <summary>
        /// Loads the game menu
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Vector2 stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Texture2D tex = this.Content.Load<Texture2D>("images/background");
            Vector2 speed1 = new Vector2(2, 0);
            Rectangle srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            Vector2 pos1 = new Vector2(0, stage.Y - srcRect.Height);
            BackgroundParell sb1 = new BackgroundParell(this, _spriteBatch, tex, pos1, srcRect, speed1);
            this.Components.Add(sb1);


            // TODO: use this.Content to load your game content here
            // instantiate all scenes here
            startScene = new StartScene(this);
            this.Components.Add(startScene);

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            highScoreScene = new HighScoreScene(this);
            this.Components.Add(highScoreScene);

            playMenuScene = new PlayMenuScene(this);
            this.Components.Add(playMenuScene);

            creditsScene = new CreditsScene(this);
            this.Components.Add(creditsScene);

            startScene.show();


        }

        /// <summary>
        /// Update method for when the user interacts with the menu
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    playMenuScene.show();

                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    helpScene.show();

                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    highScoreScene.show();

                }
                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    creditsScene.show();

                }
                else if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            if (playMenuScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    playMenuScene.hide();
                    startScene.show();
                }

            }
            if (highScoreScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    highScoreScene.hide();
                    startScene.show();
                }

            }

            if (helpScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    helpScene.hide();
                    startScene.show();
                }

            }
            if (creditsScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    creditsScene.hide();
                    startScene.show();
                }

            }
            // same way other scenes

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game menu to the user
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}