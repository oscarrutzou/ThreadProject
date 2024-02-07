using System;
using System.Collections.Generic;
//using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThreadGame
{
    public abstract class Animation
    {
        public AnimNames animationName { get; internal set; }
        public int currentFrame { get; internal set; }
        public bool isLooping;
        public Action onAnimationDone;
        public float frameRate = 5f;
        public float frameDuration { get; internal set; }
        internal float timer;
        public bool shouldPlay = true;

        public abstract int GetDimensionsWidth();
        public abstract int GetDimensionsHeight();
        public abstract void AnimationUpdate();
        public abstract void Draw(bool isCentered, Vector2 pos, Color color, float rotation, float scale, SpriteEffects spriteEffects, float layerDepth);
        public abstract Animation Clone();

    }
}
