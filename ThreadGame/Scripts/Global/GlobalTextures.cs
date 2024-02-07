using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadGame
{
    public enum TextureNames
    {
        TestPerson,
        StaticBtn,

    }

    // Dictionary of all textures
    public static class GlobalTextures
    {
        public static Dictionary<TextureNames, Texture2D> textures { get; private set; }
        public static SpriteFont defaultFont { get; private set; }

        public static void LoadContent()
        {
            ContentManager content = GameWorld.Instance.Content;
            // Load all textures
            textures = new Dictionary<TextureNames, Texture2D>
            {
                {TextureNames.TestPerson, content.Load<Texture2D>("Persons\\tile_dolphin") },
                {TextureNames.StaticBtn, content.Load<Texture2D>("UI\\ButtonAnim\\MediumBtn_0") },

            };

            // Load all fonts
            defaultFont = content.Load<SpriteFont>("Fonts\\Ariel");
        }
    }
}
