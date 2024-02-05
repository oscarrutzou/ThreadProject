using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThreadGame
{
    public abstract class GameObject
    {
        #region Variables
        public Vector2 position;
        public Color color = Color.White;
        public float rotation;
        public SpriteEffects spriteEffects = SpriteEffects.None;
        public Texture2D texture;
        public Animation animation;
        public int scale = 3;
        internal float layerDepth = 0;

        public bool isRemoved;
        public bool isVisible = true;

        private Vector2 origin;
        public bool isCentered;

        internal int collisionBoxWidth;
        internal int collisionBoxHeight;
        private Vector2 offset;
        #endregion

        public Rectangle collisionBox
        {
            get
            {
                int width;
                int height;
                // If the collision box width or height is bigger 0, use the width and height of the texture.
                if (animation != null)
                {
                    width = collisionBoxWidth > 0 ? collisionBoxWidth : animation.GetDimensionsWidth();
                    height = collisionBoxHeight > 0 ? collisionBoxHeight : animation.GetDimensionsHeight();
                }
                else if (texture != null)
                {
                    width = collisionBoxWidth > 0 ? collisionBoxWidth : texture.Width;
                    height = collisionBoxHeight > 0 ? collisionBoxHeight : texture.Height;
                }
                else
                    throw new InvalidOperationException("GameObject must have a valid texture or animation.");

                origin = isCentered ? new Vector2(width / 2, height / 2) : Vector2.Zero;
                return new Rectangle(
                    (int)(position.X + offset.X - origin.X * scale),
                    (int)(position.Y + offset.Y - origin.Y * scale),
                    (width * scale),
                    (height * scale)
                );
            }
            set { }
        }

        public virtual void Update() { }

        public virtual void Draw()
        {
            if (!isVisible) return;
            
            //Draw the animation texture or the staic texture 
            if (animation != null)
            {
                animation.Draw(isCentered, position, color, rotation, scale, spriteEffects, layerDepth);

            } else if (texture != null)
            {
                origin = isCentered ? new Vector2(texture.Width / 2, texture.Height / 2) : Vector2.Zero;
                GameWorld.Instance.spriteBatch.Draw(texture, position, null, color, rotation, origin, scale, spriteEffects, layerDepth);
            }

            //DrawDebugCollisionBox(Color.Black);
        }

        public void SetCollisionBox(int width, int height)
        {
            collisionBoxWidth = width;
            collisionBoxHeight = height;
        }

        public void SetCollisionBox(int width, int height, Vector2 offset)
        {
            collisionBoxWidth = width;
            collisionBoxHeight = height;
            this.offset = offset;
        }
        public virtual void CheckCollisionBox() { }

        public void RotateTowardsTarget(Vector2 target)
        {
            if (position == target) return;

            Vector2 dir = target - position;
            rotation = (float)Math.Atan2(-dir.Y, -dir.X) + MathHelper.Pi;
        }

        internal void DrawDebugCollisionBox(Color color)
        {
            //This has been done in a weird way, because we at the start tried to use rotatiting box colliders.
            // Draw debug collision box
            Texture2D pixel = new Texture2D(GameWorld.Instance.gfxDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            // Get the corners of the rectangle
            Vector2[] corners = new Vector2[4];
            corners[0] = new Vector2(collisionBox.Left, collisionBox.Top);
            corners[1] = new Vector2(collisionBox.Right, collisionBox.Top);
            corners[2] = new Vector2(collisionBox.Right, collisionBox.Bottom);
            corners[3] = new Vector2(collisionBox.Left, collisionBox.Bottom);

            // Rotate the corners around the center of the rectangle
            Vector2 origin = new Vector2(collisionBox.Center.X, collisionBox.Center.Y);
            for (int i = 0; i < 4; i++)
            {
                Vector2 dir = corners[i] - origin;
                corners[i] = dir + origin;
            }

            // Draw the rotated rectangle
            for (int i = 0; i < 4; i++)
            {
                Vector2 start = corners[i];
                Vector2 end = corners[(i + 1) % 4];
                DrawLine(pixel, start, end, color);
            }
        }

        internal void DrawLine(Texture2D pixel, Vector2 start, Vector2 end, Color color)
        {
            float length = Vector2.Distance(start, end);
            float angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            GameWorld.Instance.spriteBatch.Draw(pixel, start, null, color, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 1);
        }
    }
}
