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
    public class Finish : Scene
    {
        // Declare a sprite font for rendering the text
        SpriteFont textFont;

        // Declare a vector to hold the position of the
        // upper left corner of the text
        Vector2 textPosition;

        public Finish(Game game)
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

            // Create the text font and its position
            textFont = game1.Content.Load<SpriteFont>(@"Tahoma");
            textPosition.X = game1.GraphicsDevice.Viewport.Width / 2 - 75;
            textPosition.Y = game1.GraphicsDevice.Viewport.Height / 2;
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
            GraphicsDevice.Clear(Color.Red);

            // TODO: Add your drawing code here

            // Draw the text at the text position
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);
            spriteBatch.DrawString(textFont, "Game Over", textPosition,
                    Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
