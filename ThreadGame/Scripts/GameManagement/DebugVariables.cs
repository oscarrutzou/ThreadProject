using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System;
using System.Linq;

namespace ThreadGame
{
    public static class DebugVariables
    {
        private static Vector2 pos;

        public static void DrawDebug()
        {
            pos = new Vector2(10, 400);
            Process currentProcess = Process.GetCurrentProcess();
            DrawString($"Threads: {currentProcess.Threads.Count}");

        }

        private static void DrawString(string text)
        {
            GameWorld.Instance.spriteBatch.DrawString(GlobalTextures.defaultFont, text, pos, Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            Vector2 size = GlobalTextures.defaultFont.MeasureString(text);
            pos.Y += size.Y;
        }
    }
}
