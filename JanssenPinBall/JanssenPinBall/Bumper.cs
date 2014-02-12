using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace JanssenPinBall
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Bumper : Microsoft.Xna.Framework.GameComponent
    {
        // Variable that points back to the Play Scene
        protected Play playScene;

        protected string soundName;

        public Bumper(Play play)
            : base(play.game1)
        {
            // TODO: Construct any child components here

            playScene = play;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the bumper to determine if it has been hit by a pin ball.
        /// Returns true if hit by pin ball
        /// </summary>
        /// <param name="pinBall">The moving pin ball.</param>
        public virtual bool Hit(PinBall pinBall, ref Vector2 normal)
        {
            return false;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the bumper should draw itself.
        /// </summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.
        /// </param>
        public virtual void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
        }
    }
}
