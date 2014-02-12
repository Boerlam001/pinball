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
    public class Flipper : Microsoft.Xna.Framework.GameComponent
    {
        // Variable that points back to the Play Scene 
        protected Play playScene;

        // Declare a string to hold the texture name 
        string flipperTextureName;

        // Declare a texture sprite to be rendered on the play screen 
        Texture2D flipperTexture;

        Vector2[] flipperPoints;
        Vector2 flipperPivot;

        protected enum FlipperState { UP, DOWN, STOP };
        protected FlipperState flipperState;
        protected float flipperRestAngle;
        protected float flipperMaxAngle;
        protected float flipperAngle;
        protected float flipperDelta;
        protected float flipperAngularSpeed;
        protected bool OK;
        protected float flipperBounceFactor;

        Vector2 flipperOrigin;
        float flipperLength;
        float[] flipperPtRadius;
        float[] flipperPtTheta;

        public Flipper(Play play, Vector2[] points, Vector2 pivot, Vector2 origin, string textureName)
            : base(play.game1)
        {
            // TODO: Construct any child components here 
            playScene = play;
            flipperPoints = points;
            flipperPivot = pivot;
            flipperOrigin = origin;
            flipperTextureName = textureName; 
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            flipperTexture = playScene.game1.Content.Load<Texture2D>(flipperTextureName);
            flipperState = FlipperState.STOP;
            OK = true;
            flipperPtTheta = new float[flipperPoints.Count()];
            flipperPtRadius = new float[flipperPoints.Count()];
            flipperAngle = flipperRestAngle;
            for (int i = 0; i < flipperPoints.Count(); i++)
            {
                flipperPtTheta[i] = (float)Math.Atan2((flipperPoints[i].Y - flipperPivot.Y), (flipperPoints[i].X - flipperPivot.X));
                flipperPtRadius[i] = (flipperPoints[i] - flipperPivot).Length();
                flipperPoints[i].X = flipperPivot.X + flipperPtRadius[i] * (float)Math.Cos(flipperPtTheta[i] - flipperAngle);
                flipperPoints[i].Y = flipperPivot.Y + flipperPtRadius[i] * (float)Math.Sin(flipperPtTheta[i] - flipperAngle);
            }
            flipperAngularSpeed = 0.09f;
            flipperAngularSpeed = (float)Math.PI / 25;
            flipperBounceFactor = 0.9f;
            flipperLength = flipperPtRadius[1]; 

            base.Initialize();
        }

        public bool Hit(PinBall pinBall, ref Vector2 normal, ref float bounceFactor) 
        { 
            Vector2 closestPoint; 
            float ratio = ((flipperPoints[0].Y - pinBall.ballCenter.Y) * (flipperPoints[0].Y - flipperPoints[1].Y) -(flipperPoints[0].X - pinBall.ballCenter.X)
                         * (flipperPoints[1].X - flipperPoints[0].X)) / (flipperPoints[0] - flipperPoints[1]).LengthSquared(); 
            if (ratio <= 0) 
                closestPoint = flipperPoints[0]; 
            else if (ratio >= 1) 
                closestPoint = flipperPoints[1]; 
            else 
            { 
                closestPoint.X = flipperPoints[0].X + ratio * (flipperPoints[1].X - flipperPoints[0].X); 
                closestPoint.Y = flipperPoints[0].Y + ratio * (flipperPoints[1].Y - flipperPoints[0].Y); 
            } 
            Vector2 norm = pinBall.ballCenter - closestPoint; 
            if (norm.Length() > pinBall.ballRadius) 
                return false; 
            norm.Normalize(); 
            if (Vector2.Dot(norm, pinBall.ballSpeed) > 0) 
                return false; 
            normal = norm; 
            float dist = (flipperPivot - closestPoint).Length(); 
            bounceFactor = flipperBounceFactor;
            // Add code here to modify the bounceFactor 

            return true;
        } 

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            //System.Console.WriteLine("delta = " + flipperDelta); 
            if (flipperDelta != 0.0f)
            {
                flipperAngle += flipperDelta;
                for (int i = 0; i < flipperPoints.Count(); i++)
                {
                    flipperPoints[i].X = flipperPivot.X + flipperPtRadius[i] * (float)Math.Cos(flipperPtTheta[i] - flipperAngle);
                    flipperPoints[i].Y = flipperPivot.Y + flipperPtRadius[i] * (float)Math.Sin(flipperPtTheta[i] - flipperAngle);
                }
            }

            base.Update(gameTime);
        }

        /// <summary> 
        /// This is called when the flipper should draw itself. 
        /// </summary> 
        /// <param name="gameTime">Provides a snapshot of timing values.</param> 
        public virtual void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here 

            playScene.spriteBatch.Draw(flipperTexture, flipperPivot, null,
            Color.White, -flipperAngle, flipperOrigin, 1.0f, SpriteEffects.None, 1.0f);
        } 

    }
}
