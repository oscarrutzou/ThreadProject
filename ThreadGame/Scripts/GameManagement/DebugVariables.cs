using Microsoft.Xna.Framework;
using System.Linq;

namespace ThreadGame
{
    public static class DebugVariables
    {
        private static Vector2 pos;
        public static double herbivoresAlive;

        public static Color selectedGridColor = Color.Green;
        public static Color debugNonWalkableTilesColor = Color.DeepPink;

        public static void DrawDebug()
        {
            pos = new Vector2(10, 10);

            DrawString($"GameSpeed: {GameWorld.Instance.gameSpeed}");
            
        }

        private static void DrawString(string text)
        {
            GameWorld.Instance.spriteBatch.DrawString(GlobalTextures.defaultFont, text, pos, Color.Black);
            Vector2 size = GlobalTextures.defaultFont.MeasureString(text);
            pos.Y += size.Y;
        }
    }
}
