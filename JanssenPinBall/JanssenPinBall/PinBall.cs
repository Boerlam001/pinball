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
    /// This is a drawable game component that implements IUpdateable.
    /// </summary>
    public class PinBall : Microsoft.Xna.Framework.DrawableGameComponent
    {
        // Variable that points back to the Play Scene
        Play play1;

        // Declare a texture sprite to be rendered on the start screen
        Texture2D ballTexture;

        // Declare a vectore to hold the position of the center of hte ball
        public Vector2 ballCenter = Vector2.Zero;

        // Declare a vector to hold the position of the upper left corner of
        // the sprite
        Vector2 ballPosition = Vector2.Zero;

        // Declare a vector to hold the radius of the ball
        public float ballRadius;

        // Declare a speed Vector
        public Vector2 ballSpeed = Vector2.Zero;

        // Declare gravity and friction factors
        protected int gravityFactor;
        protected float frictionFactor;

        protected bool launched;

        public PinBall(Play play, Vector2 center)
            : base(play.game1)
        {
            // TODO: Construct any child components here
            play1 = play;
            ballCenter = center;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            // Create the ball sprite and its position
            ballTexture = play1.game1.Content.Load<Texture2D>("SilverBall50x50");
            ballRadius = ballTexture.Width / 2;
            ballPosition.X = ballCenter.X - ballRadius;
            ballPosition.Y = ballCenter.Y - ballRadius;
            ballSpeed = new Vector2(0.0f, 0.0f);
            gravityFactor = 2;
            frictionFactor = 1.0f;
            launched = false;

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            // Launch the ball
            if (!launched)
            {
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    ballSpeed.Y = -400f;
                    ballSpeed.X = -100f;
                    launched = true;
                }
            }


            // Check collisions with window edges
            if (ballCenter.X - ballRadius < 0)
                ballSpeed.X *= -1;
            if (ballCenter.Y - ballRadius < 0)
                ballSpeed.Y *= -1;
            if (ballCenter.X + ballRadius > play1.game1.GraphicsDevice.Viewport.Width)
                ballSpeed.X *= -1;
            if (ballCenter.Y + ballRadius > play1.game1.GraphicsDevice.Viewport.Height)
                ballSpeed.Y *= -0.5f;
            ballCenter += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            ballPosition.X = ballCenter.X - ballRadius;
            ballPosition.Y = ballCenter.Y - ballRadius;

            // Adjust ball speed
            ballSpeed *= frictionFactor;
            ballSpeed.Y += gravityFactor;

            // Bouncing
            Vector2 normal = Vector2.Zero;
            float bounceFactor = 2.5f;
            for (int i = 0; i < play1.numDrawBumpers; i++)
                if (play1.bumpers[i].Hit(this, ref normal))
                    ballSpeed = Vector2.Reflect(ballSpeed, normal);
            if (play1.leftFlipper.Hit(this, ref normal, ref bounceFactor))
                ballSpeed = Vector2.Reflect(ballSpeed, normal);
            if (play1.rightFlipper.Hit(this, ref normal, ref bounceFactor))
                ballSpeed = Vector2.Reflect(ballSpeed, normal);
            // Bounce 
            // ballSpeed *= bounceFactor; 


            if (ballCenter.Y > play1.game1.GraphicsDevice.Viewport.Height)
            {
                this.Initialize();
                play1.turns--;
            }
            for (int i = 0; i < play1.numPoints; i++)
                play1.score += play1.points[i].In(this); 


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            //play1.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);
            play1.spriteBatch.Draw(ballTexture, ballPosition, Color.White);
            //play1.spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
