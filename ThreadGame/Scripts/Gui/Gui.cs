using System;
using Microsoft.Xna.Framework;

namespace ThreadGame
{
    public class Gui: GameObject
    {
        public string text;
        public Action onClick;
        public Color textColor = Color.Black;

        public Gui()
        {
            layerDepth = 0.99f;
        }
    }
}
