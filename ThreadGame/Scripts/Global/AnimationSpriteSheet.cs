using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThreadGame
{
    public class AnimationSpriteSheet : Animation
    {
        public int frameDimensions;
        public Texture2D texture;
        private Rectangle sourceRectangle;

        public AnimationSpriteSheet(Texture2D texture, int dimensions, AnimNames name)
        {
            this.texture = texture;
            frameDimensions = dimensions;
            animationName = name;
            sourceRectangle = new Rectangle(0, 0, frameDimensions, frameDimensions);
        }

        public override void AnimationUpdate()
        {
            if (!shouldPlay) return;

            frameDuration = 1f / frameRate;
            timer += (float)GameWorld.Instance.gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > frameDuration)
            {
                timer -= frameDuration;
                int maxFrames = texture.Width / frameDimensions;
                currentFrame = (currentFrame + 1) % maxFrames;
                sourceRectangle.X = currentFrame * frameDimensions;
                CheckAnimationDone(maxFrames);
            }
        }

        public override void PauseAnim()
        {
            currentFrame = 0;
            sourceRectangle.X = currentFrame * frameDimensions;
        }

        public void CheckAnimationDone(int maxFrames)
        {
            if (currentFrame == maxFrames - 1)
            {
                if (!isLooping) frameRate = 0f;
                onAnimationDone?.Invoke();
            }
        }

        public override void Draw(bool isCentered, Vector2 pos, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth)
        {
            Vector2 origin = isCentered ? new Vector2(frameDimensions / 2, frameDimensions / 2) : Vector2.Zero;
            GameWorld.Instance.spriteBatch.Draw(texture, pos, sourceRectangle, color, rotation, origin, scale, spriteEffects, layerDepth);
        }

        public override int GetDimensionsWidth() => frameDimensions;
        public override int GetDimensionsHeight() => frameDimensions;

        public override Animation Clone()
        {
            return new AnimationSpriteSheet(texture, frameDimensions, animationName);
        }

    }

}
