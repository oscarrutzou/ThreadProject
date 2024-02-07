using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadGame
{
    public class WorkRessource: GameObject
    {
        public WorkRessource(Vector2 pos, TextureNames textureName, float scale)
        {
            this.position = pos;
            texture = GlobalTextures.textures[textureName];
            this.scale = scale;
            isCentered = true;
            layerDepth = 0.2f;
        }

        public WorkRessource(Vector2 pos, AnimNames animName, float scale)
        {
            this.position = pos;
            animation = GlobalAnimations.GetAnim(animName);
            this.scale = scale;
            isCentered = true;
            layerDepth = 0.2f;
        }
    }
}
