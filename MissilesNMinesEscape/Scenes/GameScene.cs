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
    /// Class for hanle all of scene
    /// </summary>
    public abstract class GameScene : DrawableGameComponent
    {
       
        public List<GameComponent> Components { get; set; }

        /// <summary>
        /// Shows the GameSene
        /// </summary>
        public virtual void hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }

        /// <summary>
        /// Hides the GameSene
        /// </summary>
        public virtual void show()
        {
            this.Visible = true;
            this.Enabled = true;
        }

        /// <summary>
        /// Default constructor for GameSene
        /// </summary>
        /// <param name="game"></param>
        public GameScene(Game game) : base(game)
        {
            Components = new List<GameComponent>();
            hide();
        }

        /// <summary>
        /// Checks to see if anthing needs to happen / happened
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in Components)
            {
                if (component.Enabled)
                {
                    component.Update(gameTime);
                }
            }
           base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game screen to the user
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent component in Components)
            {
                if (component is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)component;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }
    }
}
