using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadGame
{
    public class Ressource: GameObject
    {
        public Ressource(Vector2 pos, TextureNames textureName)
        {
            this.position = pos;
            texture = GlobalTextures.textures[textureName];
        }

        public Ressource(Vector2 pos, AnimNames animName)
        {
            this.position = pos;
            animation = GlobalAnimations.GetAnim(animName);
        }
    }
}
