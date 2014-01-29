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
    public class Start : Scene
    {
        // Declare a texture sprite to be rendered on the start screen
        Texture2D logoTexture;

        // Declare a vector to hold the position of the upper
        // left corner of the sprite
        Vector2 logoPosition = Vector2.Zero;

        public Start(Game game)
            : base(game)
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

            // Create the logo sprite and its position
            logoTexture = game1.Content.Load<Texture2D>("BluePinBallLogo");
            logoPosition.X = (game1.GraphicsDevice.Viewport.Width - logoTexture.Width) / 2;
            logoPosition.Y = (game1.GraphicsDevice.Viewport.Height - logoTexture.Height) / 2;


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
            if (keyboardState.IsKeyDown(Keys.Enter))
                game1.nextState = JanssenPinBall.Game1.GameState.PLAY;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here

            // Draw the logo sprite at the logo position
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);
            spriteBatch.Draw(logoTexture, logoPosition, Color.White);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
