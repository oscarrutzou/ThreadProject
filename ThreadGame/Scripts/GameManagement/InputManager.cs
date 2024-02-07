using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Threading;

namespace ThreadGame
{
    public static class InputManager
    {
        #region Variables
        private static Thread thread;

        public static KeyboardState keyboardState;
        public static KeyboardState previousKeyboardState;
        public static Vector2 desiredMoveCamDirection;
        public static MouseState mouseState;
        // Prevents multiple click when clicking a button
        public static MouseState previousMouseState;

        public static Vector2 mousePositionInWorld;
        public static Vector2 mousePositionOnScreen;
        public static bool mouseClicked;
        public static bool mouseRightClicked;

        public static bool buildMode;
        public static bool mouseOutOfBounds;
        public static bool debugStats = true;

        private static int gameSpeedIndex = 1;

        private static List<float> gameSpeed = new List<float>()
        {
            { 0.5f},
            { 1f},
            { 2f},
            { 5f},
            { 10f},
        };
        #endregion
        public static void InitHandleInput()
        {
            if (thread == null)
            {
                thread = new Thread(HandleInput);
                thread.IsBackground = true;
                thread.Start();
            }
 
        }

        private static void HandleInput()
        {
            while (true)
            {
                keyboardState = Keyboard.GetState();
                mouseState = Mouse.GetState();

                //Sets the mouse position
                mousePositionOnScreen = GetMousePositionOnUI();
                mousePositionInWorld = GetMousePositionInWorld();

                HandleKeyboardInput();
                HandleMouseInput();

                previousMouseState = mouseState;
                previousKeyboardState = keyboardState;
            }
        }

        private static void HandleKeyboardInput()
        {
            // Check if the player presses the escape key
            if (keyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape))
            {
                GameWorld.Instance.Exit();
            }

            if (keyboardState.IsKeyDown(Keys.Q) && !previousKeyboardState.IsKeyDown(Keys.Q))
            {
                debugStats = !debugStats;
            }

            desiredMoveCamDirection = GetDesiredMoveCamDirection();           
            //ChangeGameSpeed(); // Handle input and store the desired movement direction
        }

        private static void CheckButtons()
        {
            if (SceneData.guis.Count == 0) return;

            foreach (Gui gui in SceneData.guis)
            {
                if (gui is Button button)
                {
                    if (button.IsMouseOver() && button.isVisible)
                    {
                        button.OnClick();
                        return;  // Return early if a button was clicked
                    }
                }
            }
        }


        private static void ChangeGameSpeed()
        {
            if (keyboardState.IsKeyDown(Keys.Left) && !previousKeyboardState.IsKeyDown(Keys.Left))
            {
                if (gameSpeedIndex >= 1)
                {
                    gameSpeedIndex -= 1;
                    GameWorld.Instance.gameSpeed = gameSpeed[gameSpeedIndex];
                }
            }

            if (keyboardState.IsKeyDown(Keys.Right) && !previousKeyboardState.IsKeyDown(Keys.Right))
            {
                if (gameSpeedIndex < gameSpeed.Count - 1)
                {
                    gameSpeedIndex += 1;
                    GameWorld.Instance.gameSpeed = gameSpeed[gameSpeedIndex];
                }
            }
        }

        private static void HandleMouseInput()
        {
            mouseClicked = (Mouse.GetState().LeftButton == ButtonState.Pressed) && (previousMouseState.LeftButton == ButtonState.Released);
            mouseRightClicked = (Mouse.GetState().RightButton == ButtonState.Pressed) && (previousMouseState.RightButton == ButtonState.Released);

            if (mouseClicked) CheckButtons();
        }

        private static bool IsMouseOver(GameObject gameObject)
        {
            return gameObject.collisionBox.Contains(InputManager.mousePositionInWorld.ToPoint());
        }

        private static Vector2 GetMousePositionInWorld()
        {
            Vector2 pos = new Vector2(mouseState.X, mouseState.Y);
            Matrix invMatrix = Matrix.Invert(GameWorld.Instance.worldCam.GetMatrix());
            return Vector2.Transform(pos, invMatrix);
        }

        private static Vector2 GetMousePositionOnUI()
        {
            Vector2 pos = new Vector2(mouseState.X, mouseState.Y);
            Matrix invMatrix = Matrix.Invert(GameWorld.Instance.uiCam.GetMatrix());
            Vector2 returnValue = Vector2.Transform(pos, invMatrix);
            mouseOutOfBounds = (returnValue.X < 0 || returnValue.Y < 0 || returnValue.X > GameWorld.Instance.gfxManager.PreferredBackBufferWidth || returnValue.Y > GameWorld.Instance.gfxManager.PreferredBackBufferHeight);
            return returnValue;
        }

        //Need to just safe it and then allow the main thread to handle the camera since monogame's draw is not thread safe.
        private static Vector2 GetDesiredMoveCamDirection()
        {
            Vector2 moveDirection = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.W)) moveDirection.Y = -1;
            if (keyboardState.IsKeyDown(Keys.S)) moveDirection.Y = 1;
            if (keyboardState.IsKeyDown(Keys.A)) moveDirection.X = -1;
            if (keyboardState.IsKeyDown(Keys.D)) moveDirection.X = 1;
            return moveDirection;
        }


    }
}
