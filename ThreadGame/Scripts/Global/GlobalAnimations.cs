﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace ThreadGame
{
    public enum AnimNames
    {
        FighterSlash,
        FighterDead,
        Miner,
        MinerDead,
        Cook,
        CookDead,

        //GUI
        MediumButtonClick,
    }

    public static class GlobalAnimations
    {
        // Dictionary of all animations, both spritesheets and from individualFrames.
        private static Dictionary<AnimNames, Animation> animations;

        public static void LoadContent()
        {
            animations = new Dictionary<AnimNames, Animation>();
            
            LoadSpriteSheet(AnimNames.FighterSlash, "Persons\\Worker\\FighterSlash", 32);
            LoadSpriteSheet(AnimNames.FighterDead, "Persons\\Worker\\FigtherDead", 32);
            LoadIndividualFramesAnimation(AnimNames.MediumButtonClick, "UI\\ButtonAnim\\MediumBtn_", 4);
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

            animations.Add(animName, spriteSheet);
        }

        /// <summary>
        /// How to use. Each animation should be called _0, then _1 and so on, on each texuture.
        /// Remember the path should show everything and just delete the number. But keep the "_".
        /// </summary>
        /// <param name="animationName"></param>
        /// <param name="path"></param>
        /// <param name="framesInAnim"></param>
        private static void LoadIndividualFramesAnimation(AnimNames animationName, string path, int framesInAnim)
        {
            // Load all frames in the animation
            List<Texture2D> animList = new List<Texture2D>();
            for (int i = 0; i < framesInAnim; i++)
            {
                animList.Add(GameWorld.Instance.Content.Load<Texture2D>(path + i));
            }

            AnimationIndividualFrames anim = new AnimationIndividualFrames(animList, animationName);
            animations.Add(animationName, anim);
        }

        public static Animation GetAnim(AnimNames animName)
        {
            return animations[animName].Clone();
        }
    }
}
