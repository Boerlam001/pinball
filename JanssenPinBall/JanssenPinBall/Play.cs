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
    public class Play : Scene
    {
        public PinBall pinBall;
        public List<Points> points = new List<Points>();
        public int numPoints = 0;
        int numDrawPoints = 0;
        public List<Bumper> bumpers = new List<Bumper>();
        public int numDrawBumpers = 0;

        // Declare a sprite font for rendering the text 
        SpriteFont textFont;

        // Declare a texture sprite to hold the background image
        Texture2D backgroundTexture;

        // Declare a texture sprite to hold the foreground image
        Texture2D foregroundTexture;

        // Declare an integer to hold the number of points scored by the 
        // current player 
        public int score;

        // Declare an integer to hold the number of turns remaining for the 
        // current player 
        public int turns;

        // Create Vectors to hold the text positions 
        public Vector2 scoreTextPosition = new Vector2(50.0f, 550.0f);
        public Vector2 turnsTextPosition = new Vector2(600.0f, 550.0f); 

        // Declare a LeftFlipper variable
        public LeftFlipper leftFlipper;

        // Declare a RightFlipper variable
        public RightFlipper rightFlipper;

        public Play(Game game)
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
            foregroundTexture = game1.Content.Load<Texture2D>("Foreground800x600");
            backgroundTexture = game1.Content.Load<Texture2D>("Background800x600");

            pinBall = new PinBall(this, new Vector2(743, 535));
            game1.Components.Add(pinBall);

            // Initialize bumpers


            // Initialize points
            points.Add(new Points(this, new Vector2(200.0f, 300.0f), 40, 20, new string[] { "Circle110Blue", "Circle110BlueGrad", "Circle110BlueGrad" }));
            points.Add(new Points(this, new Vector2(350.0f, 325.0f), 50, 30, new string[] { "Circle110Blue", "Circle110BlueGrad", "Circle110BlueGrad" }));
            points.Add(new Points(this, new Vector2(500.0f, 300.0f), 40, 30, new string[] { "Circle110Blue", "Circle110BlueGrad", "Circle110BlueGrad" }));
            numPoints = points.Count();
            numDrawPoints = points.Count();
            for (int i = 0; i < numPoints; i++)
                points[i].Initialize();

            textFont = game1.Content.Load<SpriteFont>(@"Tahoma");
            score = 0;
            turns = 5;

            // Initialize the flippers
            leftFlipper = new LeftFlipper(this, new Vector2[] { new Vector2(200.0f, 500.0f), new Vector2(312.0f, 515.0f), new Vector2(200.0f, 530.0f),
                                          new Vector2(192.0f, 515.0f) }, new Vector2(200.0f, 515.0f), new Vector2(9, 15), "LeftFlipper120x30");
            game1.Components.Add(leftFlipper);

            rightFlipper = new RightFlipper(this, new Vector2[] { new Vector2(500.0f, 500.0f), new Vector2(388.0f, 515.0f), new Vector2(500.0f, 530.0f),
                                            new Vector2(508.0f, 515.0f) }, new Vector2(500.0f, 515.0f), new Vector2(111, 15), "RightFlipper120x30");
            game1.Components.Add(rightFlipper); 


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
            if (keyboardState.IsKeyDown(Keys.Escape))
                game1.nextState = JanssenPinBall.Game1.GameState.FINISH;

            if (turns < 1)
                game1.nextState = JanssenPinBall.Game1.GameState.FINISH;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            for (int i = 0; i < numDrawBumpers; i++)
                bumpers[i].Draw(gameTime);
            for (int i = 0; i < numDrawPoints; i++)
                points[i].Draw(gameTime);

            leftFlipper.Draw(gameTime);
            rightFlipper.Draw(gameTime);

            spriteBatch.DrawString(textFont, "Score: " + score, scoreTextPosition, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 1);
            spriteBatch.DrawString(textFont, "Turns: " + turns, turnsTextPosition, Color.Black, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 1);

            pinBall.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
