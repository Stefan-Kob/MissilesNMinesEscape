using MissilesNMinesEscape.Entities;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.IO;
using MissilesNMinesEscape.Shared;

namespace MissilesNMinesEscape.Scenes
{
    /// <summary>
    /// Menu that is brought up after selecting to play a game
    /// </summary>
    public class PlayMenuScene : GameScene
    {
        EasyModeScene easyModeScene;
        HardModeScene hardModeScene;     

        private MenuComponent menu;
        public MenuComponent Menu { get => menu; set => menu = value; }

        private SpriteBatch _spriteBatch;
        private Texture2D image;
        SpriteFont normalFont;

        Game1 g;


        
        /// <summary>
        /// Loads the selection screen for if the user wants hard mode or easy mode
        /// </summary>
        /// <param name="game"></param>
        public PlayMenuScene(Game game) : base(game)
        {
            g = (Game1)game;
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);

            image = g.Content.Load<Texture2D>("images/Intro");

            easyModeScene = new EasyModeScene(g);
            g.Components.Add(easyModeScene);
           
            hardModeScene = new HardModeScene(g);
            g.Components.Add(hardModeScene);
            

            normalFont = game.Content.Load<SpriteFont>("fonts/ExpainFont");
            SpriteFont modelNormalFont = g.Content.Load<SpriteFont>("fonts/GameModeNormalFont");
            SpriteFont modeSelectedFont = g.Content.Load<SpriteFont>("fonts/GameModeSelectedFont");
            string[] menuItems = { "Easy Mode","Hard Mode" };
            Menu = new MenuComponent(g, g._spriteBatch, modelNormalFont, modeSelectedFont, menuItems,"PlayMenu");
            this.Components.Add(Menu);
        }

        /// <summary>
        /// Handles updating the users mouse
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (easyModeScene!=null)
            {
                easyModeScene = new EasyModeScene(g);
                Game.Components.Add(easyModeScene);
            }
            if (hardModeScene != null)
            {
                hardModeScene = new HardModeScene(g);
                Game.Components.Add(hardModeScene);

            }
            if (this.Enabled)
            {
                HandleInput();
                base.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws the play menu to the user
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            string info = "Please click space to choose Game Mode";
            string alert = "To Go to Menu in Game, Press ESC";
            _spriteBatch.DrawString(normalFont,info, new Vector2(40, 30), Color.Black);
            _spriteBatch.DrawString(normalFont, alert, new Vector2(40, 60), Color.Red);
            _spriteBatch.Draw(image, new Vector2(45, 130), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Handles the imput for the easy or hard menu
        /// </summary>
        private void HandleInput()
        {
            int selectedIndex = -1;
            KeyboardState ks = Keyboard.GetState();
           
            selectedIndex = Menu.SelectedIndex;

            
            if (selectedIndex == 0 && ks.IsKeyDown(Keys.Space))
            {
                this.hide();
                easyModeScene.show();
                PlayMusic();

            }
            else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Space))
            {
                this.hide();
                hardModeScene.show();
                PlayMusic();

            }

        }
        
        /// <summary>
        /// Method for playing the game music when loading into a level
        /// </summary>
        public void PlayMusic()
        {
            Song backgroundMusic = g.Content.Load<Song>("audios/daftyMusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
        }
    }
}
