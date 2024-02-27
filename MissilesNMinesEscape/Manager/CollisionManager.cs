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
using MissilesNMinesEscape.Entities;
using MissilesNMinesEscape.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissilesNMinesEscape
{
    /*
     *CollisionManager.cs
     *CollisionManager class to check to see if anthing is intersecting and the opertaion if so 
     *
     *Revision History : Finished
     *Kihoon/Stefan, 2023/12/4: Created
     */
    /// <summary>
    /// Class for handle collision with obstacle
    /// </summary>
    public class CollisionManager : GameComponent
    {
        private Game game;

        private List<Missile> missileList;
        private List<Coin> coinList;
        private List<MineBomb> mineBombList;
        private List<Helli> helliList;
        private List<Bullet> bulletList;
        private Airplane airplane;
        private MineBomb mineBomb;
        private Bullet bullet;
        private GameScene gameScene;
        private Helli helli;
       
        private bool seccondConstructor = false;

        private EasyModeScene easyModeScene { get; set; }
        /// <summary>
        /// Constructor 1, used by the easyStartScene to configure the CollisionManager
        /// </summary>
        /// <param name="game">game object</param>
        /// <param name="missileList">The list of missile to handle collision</param>
        /// <param name="mineBombList">The list of mine to handle collision</param>
        /// <param name="coinList">The list of coin to handle collision</param>
        /// <param name="mineBomb">The list of mine to handle collision</param>
        /// <param name="airplane">The list of plane to handle collision</param>
        /// <param name="gameScene">The selected secene easymode</param>
        public CollisionManager(Game game, List<Missile> missileList, List<MineBomb> mineBombList, List<Coin> coinList, MineBomb mineBomb, Airplane airplane, GameScene gameScene) : base(game)
        {
            this.coinList = coinList;
            this.game = game;
            this.airplane = airplane;
            this.mineBomb = mineBomb;
            this.missileList = missileList;
            this.mineBombList = mineBombList;
            this.gameScene = gameScene;
        }

        /// <summary>
        /// Constructor 2, used by the hardStartScene to configure the CollisionManager
        /// </summary>
        /// <param name="game">game object</param>
        /// <param name="missileList">The list of missile to handle collision</param>
        /// <param name="mineBombList">The list of mine to handle collision</param>
        /// <param name="helliList">The list of helli to handle collision</param>
        /// <param name="coinList">The list of coin to handle collision</param>
        /// <param name="bulletList">The list of bullet to handle collision</param>
        /// <param name="mineBomb">The list of mine to handle collision</param>
        /// <param name="airplane">The list of plane to handle collision</param>
        /// <param name="gameScene">the selected scene</param>
        /// <param name="helli">the heli object in gameScene</param>
        /// <param name="bullet">the bullet object in gameScene</param>
        public CollisionManager(Game game, List<Missile> missileList, List<MineBomb> mineBombList, List<Helli> helliList, List<Coin> coinList, List<Bullet> bulletList, MineBomb mineBomb, Airplane airplane, GameScene gameScene, Helli helli, Bullet bullet) : base(game)
        {
            this.helli = helli;
            this.helliList = helliList;
            this.coinList = coinList;
            this.game = game;
            this.airplane = airplane;
            this.mineBomb = mineBomb;
            this.missileList = missileList;
            this.mineBombList = mineBombList;
            this.gameScene = gameScene;
            this.bulletList = bulletList;
            this.bullet = bullet;
            seccondConstructor = true;
        }

        /// <summary>
        /// Method that updates to check for collisions
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Rectangle bulletRect = new Rectangle();
            Rectangle helliRect = new Rectangle();
            Rectangle missileRect = new Rectangle();
            Rectangle bombRect = new Rectangle();
            Rectangle coinRect = new Rectangle();
            Rectangle airplaneRect = airplane.getBounds();

            foreach (Missile m in missileList)
            {
                missileRect = m.getBounds();
                if (airplaneRect.Intersects(missileRect))
                {
                    // Put code for what happens on an inersection here
                    airplane.Visible = false;
                    airplane.Enabled = false;
                    HandleCollision();
                    break;
                }
            }
            foreach (Coin c in coinList)
            {
                coinRect = c.getBounds();

                if (airplaneRect.Intersects(coinRect))
                {
                    c.Visible = false;
                    c.Enabled = false;
                }
            }
            foreach (MineBomb b in mineBombList)
            {
                bombRect = b.getBounds();
                if (airplaneRect.Intersects(bombRect))
                {
                    // Put code for what happens on an inersection here
                    airplane.Visible = false;
                    airplane.Enabled = false;
                    HandleCollision();
                    break;
                }
            }
            if (seccondConstructor == true)
            {
                foreach (Helli h in helliList)
                {
                    helliRect = h.getBounds();
                    if (airplaneRect.Intersects(helliRect))
                    {
                        // Put code for what happens on an inersection here
                        helli.Visible = false;
                        helli.Enabled = false;
                        HandleCollision();
                        break;
                    }
                }
                foreach (Bullet b in bulletList)
                {
                    bulletRect = b.getBounds();
                    if (airplaneRect.Intersects(bulletRect))
                    {
                        // Put code for what happens on an inersection here
                        airplane.Visible = false;
                        airplane.Enabled = false;
                        HandleCollision();
                        break;
                    }
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Method that gets called when a collision takes place
        /// </summary>
        private void HandleCollision()
        {
            // 게임 모드에 따른 처리를 수행합니다.
            if (gameScene is EasyModeScene)
            {
                EasyModeScene easyModeScene = (EasyModeScene)gameScene;
                easyModeScene.EndGame();
            }
            else if (gameScene is HardModeScene)
            {
                HardModeScene hardModeScene = (HardModeScene)gameScene;
                hardModeScene.EndGame();
            }
        }
    }
}