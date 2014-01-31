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
    public class Points : Microsoft.Xna.Framework.GameComponent
    {
        // Variable that points back to the Play Scene 
        protected Play playScene;

        // Declare an array of texture sprites to be rendered on 
        // the play screen; multple sprites will be used to make the 
        // points image flash 
        Texture2D[] pointsTexture;

        // Declare an of strings to hold the texture names 
        string[] pointsTextureName;

        // Declare an integer to hold the number of tectures 
        int numTextures;

        // Declare an integer to hold the index of the current texture 
        int whichTexture;

        // Declare a sprite font for rendering the text 
        SpriteFont textFont;

        // Declare a vector to hold the position of the upper left 
        // corner of the text 
        Vector2 textPosition;

        // Declare a vector to hold the position of the center of the points 
        Vector2 pointsCenter = Vector2.Zero;

        // Declare a vector to hold the position of the upper left 
        // corner of the sprite 
        Vector2 pointsPosition = Vector2.Zero;

        // Declare a float to hold the radius of the points 
        float pointsRadius;

        // Declare a float to hold the scale at which the points will 
        // be drawn 
        float pointsScale;

        // Declare an int to hold the points value 
        int pointsValue;

        // Declare a boolean to indicate the position of the ball with 
        // respect to the image 
        bool IN;

        public Points(Play play, Vector2 center, int radius, int value, string[] textureName)
            : base(play.game1)
        {
            // TODO: Construct any child components here
            playScene = play;
            pointsCenter = center;
            pointsRadius = radius;
            pointsValue = value;
            pointsTextureName = textureName;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            numTextures = pointsTextureName.Count();
            whichTexture = 0;
            pointsTexture = new Texture2D[numTextures];
            for (int i = 0; i < numTextures; i++)
                pointsTexture[i] = playScene.game1.Content.Load<Texture2D>(pointsTextureName[i]);
            textFont = playScene.game1.Content.Load<SpriteFont>(@"Tahoma");
            textPosition = pointsCenter - new Vector2(20, 20);
            pointsPosition.X = pointsCenter.X - pointsRadius;
            pointsPosition.Y = pointsCenter.Y - pointsRadius;
            pointsScale = pointsRadius / (pointsTexture[0].Width / 2.0f);
            IN = false;

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (IN)
                whichTexture = Math.Max(0, whichTexture - 1);

            base.Update(gameTime);
        }

        /// <summary> 
        /// This is called when the game should draw itself. 
        /// </summary> 
        /// <param name="gameTime">Provides a snapshot of timing values.</param> 
        public void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here 

            // Draw the logo sprite at the logo position 
            playScene.spriteBatch.Draw(pointsTexture[whichTexture], pointsPosition, null, Color.White, 0.0f, Vector2.Zero, pointsScale, SpriteEffects.None, 1.0f);
            playScene.spriteBatch.DrawString(textFont,
            pointsValue.ToString(), textPosition, Color.Blue, 0,
            Vector2.Zero, 2, SpriteEffects.None, 1.0f); 
        }

        /// <summary> 
        /// Allows the points to determine if it has been rolled over by a 
        /// pin ball. Returns the points' value if rolled over by pin ball 
        /// </summary> 
        /// <param name="pinBall">The moving pin ball.</param> 
        public int In(PinBall pinBall)
        {
            int value = 0;
            Vector2 norm = pinBall.ballCenter - pointsCenter;
            if (norm.Length() <= pointsRadius)
            {
                if (IN == false)
                    value = pointsValue;
                IN = true;
                whichTexture = numTextures - 1;
            }
            else
            {
                IN = false;
                whichTexture = 0;
            }
            return value;
        } 
    }
}
