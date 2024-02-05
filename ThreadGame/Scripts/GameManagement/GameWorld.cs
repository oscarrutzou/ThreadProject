﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace ThreadGame
{
    public class GameWorld : Game
    {
        #region Variables
        public Dictionary<ScenesNames, Scene> scenes { get; private set; }
        public Scene currentScene;
        public Camera worldCam { get; private set; }
        public Camera uiCam { get; private set; } //Static on the ui
        public GameTime gameTime { get; private set; }
        public Random random { get; private set; }

        public SpriteBatch spriteBatch;
        public GraphicsDeviceManager gfxManager;
        public GraphicsDevice gfxDevice => GraphicsDevice;
        public float gameSpeed = 1f;

        public static GameWorld Instance;
        #endregion

        public GameWorld()
        {
            if (Instance == null) Instance = this;

            gfxManager = new GraphicsDeviceManager(this);
            random = new Random();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "Thread Game";
        }


        protected override void Initialize()
        {
            ResolutionSize(1280, 720);
            //Fullscreen();
            worldCam = new Camera(new Vector2(gfxManager.PreferredBackBufferWidth / 2, gfxManager.PreferredBackBufferHeight / 2), true);
            uiCam = new Camera(Vector2.Zero, false);

            GlobalTextures.LoadContent();
            GlobalAnimations.LoadContent();

            GenerateScenes();
            currentScene = scenes[ScenesNames.OTestScene];
            currentScene.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(gfxDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            InputManager.HandleInput();
            currentScene.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            gfxDevice.Clear(Color.Beige);
            ////Draw in world objects
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, BlendState.AlphaBlend,
                SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise,
                transformMatrix: worldCam.GetMatrix());

            currentScene.DrawInWorld();
            spriteBatch.End();

            //Draw on screen objects
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, BlendState.AlphaBlend,
                SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise,
                transformMatrix: uiCam.GetMatrix());

            currentScene.DrawOnScreen();
            spriteBatch.End();

            base.Draw(gameTime);
        }


        private void GenerateScenes()
        {
            scenes = new Dictionary<ScenesNames, Scene>();
            scenes[ScenesNames.OTestScene] = new OTestScene();
            scenes[ScenesNames.JTestScene] = new JTestScene();
            scenes[ScenesNames.RTestScene] = new RTestScene();
            scenes[ScenesNames.VTestScene] = new VTestScene();
        }

        public void ResolutionSize(int width, int height)
        {
            gfxManager.HardwareModeSwitch = true;
            gfxManager.PreferredBackBufferWidth = width;
            gfxManager.PreferredBackBufferHeight = height;
            gfxManager.IsFullScreen = false;
            gfxManager.ApplyChanges();
        }

        public void Fullscreen()
        {
            gfxManager.HardwareModeSwitch = false;
            gfxManager.PreferredBackBufferWidth = gfxDevice.DisplayMode.Width;
            gfxManager.PreferredBackBufferHeight = gfxDevice.DisplayMode.Height;
            gfxManager.IsFullScreen = true;
            gfxManager.ApplyChanges();
        }
    }
}
