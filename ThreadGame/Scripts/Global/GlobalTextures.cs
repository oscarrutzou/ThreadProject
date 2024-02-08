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
        TextField1,
        TextField2,
        PixelEmpty,
        PixelWhite,

        Crysal1,
        Crysal2,
        Crysal3,
        Bush1,
        Bush2,
        Bush3,
        Grass1,
        Grass2,
        Grass3,
        ChefFood1,
        ChefFood2,

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
                { TextureNames.TextField1, content.Load<Texture2D>("UI\\textfield1")},
                { TextureNames.TextField2, content.Load<Texture2D>("UI\\textfield2")},
                { TextureNames.PixelEmpty, content.Load<Texture2D>("UI\\PixelEmpty")},
                { TextureNames.PixelWhite, content.Load<Texture2D>("UI\\PixelWhite")},
                { TextureNames.Crysal1, content.Load<Texture2D>("Persons\\Ressource\\crystal_1")},
                { TextureNames.Crysal2, content.Load<Texture2D>("Persons\\Ressource\\crystal_2")},
                { TextureNames.Crysal3, content.Load<Texture2D>("Persons\\Ressource\\crystal_3")},
                { TextureNames.Grass1, content.Load<Texture2D>("World\\grass_1")},
                { TextureNames.Grass2, content.Load<Texture2D>("World\\grass_2")},
                { TextureNames.Grass3, content.Load<Texture2D>("World\\grass_3")},
                { TextureNames.Bush1, content.Load<Texture2D>("World\\bush_1")},
                { TextureNames.Bush2, content.Load<Texture2D>("World\\bush_2")},
                { TextureNames.Bush3, content.Load<Texture2D>("World\\bush_3")},
                { TextureNames.ChefFood1, content.Load<Texture2D>("Persons\\Ressource\\chefFood_1")},
                { TextureNames.ChefFood2, content.Load<Texture2D>("Persons\\Ressource\\chefFood_2")},

            };

            // Load all fonts
            defaultFont = content.Load<SpriteFont>("Fonts\\Ariel");
        }
    }
}
