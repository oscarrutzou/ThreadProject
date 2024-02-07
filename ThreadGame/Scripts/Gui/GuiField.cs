using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThreadGame
{
    public class GuiField: Gui
    {
        public GuiField(Vector2 position, TextureNames textureName, bool isCentered, int scale)
        {
            this.position = position;
            texture = GlobalTextures.textures[textureName];
            this.isCentered = isCentered;
            this.scale = scale;
        }

        public GuiField(string text, Vector2 position)
        {
            this.position = position;
            this.text = text;
            texture = GlobalTextures.textures[TextureNames.TextField];
            isCentered = true;
        }

        public override void Draw()
        {
            base.Draw();
            DrawText();
            DrawDebugCollisionBox(Color.Black);
        }

        /// <summary>
        /// Can take and divide the text and center each part of the text.
        /// </summary>
        private void DrawText()
        {
            // If the text is not visible or null, we don't need to do anything
            if (!isVisible || text == null) return;

            // Split the text into lines based on the newline character '\n'
            string[] lines = text.Split('\n');

            // Create an array to hold the size of each line
            Vector2[] lineSizes = new Vector2[lines.Length];

            // Measure the size of each line and store it in the array
            for (int i = 0; i < lines.Length; i++)
            {
                lineSizes[i] = GlobalTextures.defaultFont.MeasureString(lines[i]);
            }

            // Find the size of the longest line by comparing the width (X value) of each line. Vector2.Zero is the seed in the Aggregate method
            Vector2 maxSize = lineSizes.Aggregate(Vector2.Zero, (max, current) => (current.X > max.X) ? current : max);

            // Calculate the total height of the text block
            float totalHeight = lines.Length * GlobalTextures.defaultFont.LineSpacing;

            // Calculate the position to center the text based on the size of the longest line and total height
            Vector2 textPosition = position - new Vector2(maxSize.X / 2, totalHeight / 2);

            // Draw each line of the text
            for (int i = 0; i < lines.Length; i++)
            {
                // Calculate the position of the line, centering it horizontally and adjusting vertically based on the line number
                Vector2 linePosition = textPosition + new Vector2((maxSize.X - lineSizes[i].X) / 2, i * GlobalTextures.defaultFont.LineSpacing);

                // Draw the line of text
                GameWorld.Instance.spriteBatch.DrawString(GlobalTextures.defaultFont,
                                                          lines[i],
                                                          linePosition,
                                                          textColor,
                                                          0,
                                                          Vector2.Zero,
                                                          1,
                                                          SpriteEffects.None,
                                                          1);
            }
        }


    }
}
