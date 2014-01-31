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
    public class LeftFlipper : Flipper
    {
        public LeftFlipper(Play play, Vector2[] points, Vector2 pivot, Vector2 origin, string textureName)
            : base(play, points, pivot, origin, textureName)
        {
            // TODO: Construct any child components here 
        } 

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            System.Console.WriteLine("Left flipper");
            flipperRestAngle = (float)Math.PI * -0.1f;
            flipperMaxAngle = (float)Math.PI * 0.1f; 

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                if (OK && flipperState == FlipperState.STOP)
                    flipperState = FlipperState.UP;
                OK = false;
            }
            else
                OK = true;
            flipperDelta = 0.0f;
            if (flipperState == FlipperState.UP)
            {
                flipperDelta = flipperAngularSpeed;
                if (flipperAngle > flipperMaxAngle)
                {
                    flipperState = FlipperState.DOWN;
                    flipperDelta = flipperMaxAngle - flipperAngle;
                }
            }
            if (flipperState == FlipperState.DOWN)
            {
                flipperDelta = -flipperAngularSpeed;
                if (flipperAngle < flipperRestAngle)
                {
                    flipperState = FlipperState.STOP;
                    flipperDelta = flipperRestAngle - flipperAngle;
                }
            } 

            base.Update(gameTime);
        }
    }
}
