using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissilesNMinesEscape.Shared
{
    /// <summary>
    /// Class for the menu logic, used in the startScene class
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont normalFont, selectedFont;

        private List<string> menuLists;

        public int SelectedIndex { get; set; }

        private Vector2 position;
        private Color normalColor = Color.Black;
        private Color selectedColor = Color.DarkRed;


        private Color modeNormalColor = Color.Navy;
        private Color modeSelectedColor = Color.Red;

        private string type;

        private KeyboardState preStage;

        /// <summary>
        /// Constructor to build the menu
        /// </summary>
        /// <param name="game">game object</param>
        /// <param name="sb">sprite batch for menu</param>
        /// <param name="regularFont">font for regural menu</param>
        /// <param name="hilightFont">font for selected menu</param>
        /// <param name="menus">menu list</param>
        /// <param name="type">scene type</param>
        public MenuComponent(Game game, SpriteBatch sb, SpriteFont regularFont, SpriteFont hilightFont, string[] menus, string type) : base(game)
        {
            if (type == "Start")
            {
                spriteBatch = sb;
                normalFont = regularFont;
                selectedFont = hilightFont;
                menuLists = menus.ToList();
                position = new Vector2(SharingComponent.stage.X / 7, SharingComponent.stage.Y / 4);
                this.type = "Start";
            }
            else if (type == "PlayMenu")
            {
                spriteBatch = sb;
                normalFont = regularFont;
                selectedFont = hilightFont;
                menuLists = menus.ToList();
                position = new Vector2(350, 100);
                this.type = "PlayMenu";
            }


        }

        /// <summary>
        /// Handles the navigation of the menu
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && preStage.IsKeyUp(Keys.Down))
            {
                SelectedIndex++;
                if (SelectedIndex == menuLists.Count)
                {
                    SelectedIndex = 0;
                }
            }

            if (ks.IsKeyDown(Keys.Up) && preStage.IsKeyUp(Keys.Up))
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = menuLists.Count - 1;
                }

            }
            if (ks.IsKeyDown(Keys.Left) && preStage.IsKeyUp(Keys.Left))
            {
                SelectedIndex++;
                if (SelectedIndex == menuLists.Count)
                {
                    SelectedIndex = 0;
                }
            }

            if (ks.IsKeyDown(Keys.Right) && preStage.IsKeyUp(Keys.Right))
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = menuLists.Count - 1;
                }

            }




            preStage = ks;
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the menu to the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 temPos = position;
            spriteBatch.Begin();
            for (int i = 0; i < menuLists.Count; i++)
            {
                if (type == "Start")
                {
                    if (SelectedIndex == i)
                    {
                        spriteBatch.DrawString(selectedFont, menuLists[i], temPos, selectedColor);
                        temPos.Y += 50;
                    }
                    else
                    {
                        spriteBatch.DrawString(normalFont, menuLists[i], temPos, normalColor);
                        temPos.Y += 50;
                    }

                }
                else if (type == "PlayMenu")
                {
                    if (SelectedIndex == i)
                    {
                        Vector2 pos = new Vector2(100, 100);
                        spriteBatch.DrawString(selectedFont, menuLists[i], temPos, modeSelectedColor);
                        temPos.X += 200;
                    }
                    else
                    {
                        Vector2 pos = new Vector2(300, 100);
                        spriteBatch.DrawString(normalFont, menuLists[i], temPos, modeNormalColor);
                        temPos.X += 200;
                    }

                }


            }
            spriteBatch.End();
            base.Update(gameTime);
        }
    }
}
