using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ThreadGame
{
    public class Button: Gui
    {
        private float maxScale = 1f;
        private float clickCooldown = 0.1f; // The delay between button clicks in seconds
        private float timeSinceLastClick = 0; // The time since the button was last clicked
        private bool invokeActionOnFullScale = true;
        private bool hasPressed;
        private float shinkToScale = 0.95f;

        public Button(string text, Action onClick, TextureNames textureName, Vector2 position)
        {
            this.position = position;
            this.text = text;
            texture = GlobalTextures.textures[textureName];
            this.onClick = onClick;
            isCentered = true;
        }

        public Button(string text, Action onClick, TextureNames textureName, Vector2 position, int scale)
        {
            this.position = position;
            this.text = text;
            texture = GlobalTextures.textures[textureName];
            this.onClick = onClick;
            isCentered = true;

            this.scale = scale;
            maxScale = scale;
            shinkToScale = maxScale * 0.95f;
        }

        public Button(string text, Action onClick, AnimNames animName, Vector2 position)
        {
            this.text = text;
            this.onClick = onClick;
            SetAnim(animName);
            this.position = position;

            isCentered = true;
        }

        public Button(Action onClick, AnimNames animName, Vector2 position, int scale)
        {
            this.text = "";
            this.onClick = onClick;
            SetAnim(animName);
            this.position = position;
            this.scale = scale;
            isCentered = true;


            maxScale = scale;
            shinkToScale = maxScale * 0.95f;
        }

        public Button(string text, Action onClick, AnimNames animName, Vector2 position, int scale)
        {
            this.text = text;
            this.onClick = onClick;
            SetAnim(animName);
            this.position = position;
            this.scale = scale;
            isCentered = true;


            maxScale = scale;
            shinkToScale = maxScale * 0.95f;
        }

        private void SetAnim(AnimNames animName)
        {
            animation = GlobalAnimations.GetAnim(animName);
            animation.frameRate = 10f;
            animation.shouldPlay = false;
            animation.isLooping = true;
        }

        public override void Update()
        {
            if (timeSinceLastClick < clickCooldown)
            {
                timeSinceLastClick += (float)GameWorld.Instance.gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (!IsMouseOver() || InputManager.mouseState.LeftButton == ButtonState.Released)
            {
                // Increase the scale by 1% each frame, up to the original size
                scale = Math.Min(maxScale, scale + 0.01f);

                if (!isVisible) return;
                if (!invokeActionOnFullScale) return;
                if (!hasPressed) return;
                if (scale != maxScale) return;

                onClick?.Invoke();
                hasPressed = false;
            }
        }


        // Check if the mouse is over the button
        public bool IsMouseOver()
        {
            return collisionBox.Contains(InputManager.mousePositionOnScreen.ToPoint());
        }

        // Called when the left mouse button is pressed
        public void OnClick()
        {
            if (!isVisible) return;

            scale = shinkToScale;  // Shrink the button
            //Play anim

            if (timeSinceLastClick >= clickCooldown)
            {
                timeSinceLastClick = 0;
                PlayClickAnimation();

                if (invokeActionOnFullScale)
                {
                    hasPressed = true;
                } else
                {
                    onClick?.Invoke();
                }
            }
        }

        public void PlayClickAnimation()
        {
            if (animation == null || animation.shouldPlay == true) return;
            
            animation.shouldPlay = true;
            animation.onAnimationDone += ResetAnim;
            
        }
        
        private void ResetAnim()
        {
            animation.currentFrame = 0;
            animation.shouldPlay = false;
            animation.onAnimationDone -= ResetAnim;
        }

        public override void Draw()
        {
            base.Draw();
            DrawText();
            //DrawDebugCollisionBox(Color.Black);
        }

        private void DrawText()
        {
            if (!isVisible) return;

            // Measure the size of the text
            Vector2 textSize = GlobalTextures.defaultFont.MeasureString(text);

            // Calculate the position to center the text
            Vector2 textPosition = position - textSize / 2;

            GameWorld.Instance.spriteBatch.DrawString(GlobalTextures.defaultFont,
                                              text,
                                              textPosition,
                                              textColor,
                                              0,
                                              Vector2.Zero,
                                              1,
                                              SpriteEffects.None,
                                              1);
        }

    }
}
