using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ThreadGame
{
    public enum AnimNames
    {
        FighterSlash,
        FighterDead,
    }

    public static class GlobalAnimations
    {
        // Dictionary of all animations, both spritesheets and from individualFrames.
        public static Dictionary<AnimNames, Animation> animationsTest { get; private set; }

        public static void LoadContent()
        {
            animationsTest = new Dictionary<AnimNames, Animation>();
            
            LoadSpriteSheet(AnimNames.FighterSlash, "Persons\\Worker\\FighterSlash", 32);
            LoadSpriteSheet(AnimNames.FighterDead, "Persons\\Worker\\FigtherDead", 32);
        }

        /// <summary>
        /// Left to right spritesheets
        /// </summary>
        /// <param name="animName"></param>
        /// <param name="path"></param>
        /// <param name="dem"></param>
        private static void LoadSpriteSheet(AnimNames animName, string path, int dem)
        {
            AnimationSpriteSheet spriteSheet = new AnimationSpriteSheet(
                GameWorld.Instance.Content.Load<Texture2D>(path),
                dem,
                animName);

            animationsTest.Add(animName, spriteSheet);
        }

        /// <summary>
        /// How to use. Each animation should be called _0, then _1 and so on, on each texuture.
        /// Remember the path should show everything and just delete the number. But keep the "_".
        /// </summary>
        /// <param name="animationName"></param>
        /// <param name="path"></param>
        /// <param name="framesInAnim"></param>
        private static void LoadIndividualFramesAnimationT(AnimNames animationName, string path, int framesInAnim)
        {
            // Load all frames in the animation
            List<Texture2D> animList = new List<Texture2D>();
            for (int i = 0; i < framesInAnim; i++)
            {
                animList.Add(GameWorld.Instance.Content.Load<Texture2D>(path + i));
            }

            AnimationIndividualFrames anim = new AnimationIndividualFrames(animList, animationName);
            animationsTest.Add(animationName, anim);
        }
    }
}
