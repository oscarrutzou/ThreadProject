using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThreadGame
{
    public class AnimationIndividualFrames: Animation
    {

        public List<Texture2D> frames { get; private set; }


        public AnimationIndividualFrames(List<Texture2D> frames, AnimNames animationName)
        {
            this.frames = frames;
            currentFrame = 0;
            this.animationName = animationName;
        }


        public override void AnimationUpdate()
        {
            // Calculate the frame duration based on the frame rate hwj
            frameDuration = 1f / frameRate;

            // Add the elapsed time since the last frame to the timer
            timer += (float)GameWorld.Instance.gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > frameDuration)
            {
                timer -= frameDuration;
                currentFrame = (currentFrame + 1) % frames.Count;
                CheckAnimationDone();
            }
        }

        /// <summary>
        /// Check if animation is done
        /// </summary>
        public void CheckAnimationDone()
        {
            if (currentFrame == frames.Count - 1)
            {
                if (!isLooping) frameRate = 0f;
                onAnimationDone?.Invoke();
            }
        }

        public override void Draw(bool isCentered, Vector2 pos, Color color, float rotation, int scale, SpriteEffects spriteEffects, float layerDepth)
        {
            Texture2D curFrame = frames[currentFrame];
            Vector2 origin = isCentered ? new Vector2(curFrame.Width / 2, curFrame.Height / 2) : Vector2.Zero;
            GameWorld.Instance.spriteBatch.Draw(curFrame, pos, null, color, rotation, origin, scale, spriteEffects, layerDepth);
        }

        /// <summary>
        /// Takes the Width of the current frame. Make sure it you dont work in equal sprites, to fix this.
        /// </summary>
        /// <returns></returns>
        public override int GetDimensionsWidth() => frames[currentFrame].Width;
        public override int GetDimensionsHeight() => frames[currentFrame].Height;
    }
}
