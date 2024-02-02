using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ThreadGame
{
    public enum AnimNames
    {
        TestAnim,
        
    }

    public static class GlobalAnimations
    {
        // Dictionary of all animations
        private static Dictionary<AnimNames, List<Texture2D>> animations = new Dictionary<AnimNames, List<Texture2D>>();
        //public static float progress = 0f;

        public static void LoadContent()
        {
            //How to use. Each animation should be called _0, then _1 and so on, on each texuture.
            //Remember the path should show everything and just delete the number. But keep the "_".
            //LoadAnimation(AnimNames.TestAnim, "path_", (int)amount of frames);
        }

        private static void LoadAnimation(AnimNames animationName, string path, int framesInAnim)
        {
            // Load all frames in the animation
            List<Texture2D> animList = new List<Texture2D>();
            for (int i = 0; i < framesInAnim; i++)
            {
                animList.Add(GameWorld.Instance.Content.Load<Texture2D>(path + i));
            }
            animations[animationName] = animList;
        }

        public static Animation SetAnimation(AnimNames name)
        {
            // Check if the animation exists
            return new Animation(animations[name], name);
        }

    }
}
