using Microsoft.Xna.Framework;
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
            pos = new Vector2(10, 10);
            Process currentProcess = Process.GetCurrentProcess();
            DrawString($"Threads: {currentProcess.Threads.Count}");
            DrawString($"GameSpeed: {GameWorld.Instance.gameSpeed}");
            DrawString($"Cam pos: {GameWorld.Instance.worldCam.position}");

            DrawString($"Money: {Ressources.money}");
            DrawString($"Food: {Ressources.food}");
            DrawString($"MonsterDrops: {Ressources.monsterDrop}");

        }

        private static void DrawString(string text)
        {
            GameWorld.Instance.spriteBatch.DrawString(GlobalTextures.defaultFont, text, pos, Color.Black);
            Vector2 size = GlobalTextures.defaultFont.MeasureString(text);
            pos.Y += size.Y;
        }
    }
}
