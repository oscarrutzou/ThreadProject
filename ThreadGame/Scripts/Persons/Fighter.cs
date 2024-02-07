using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadGame
{
    /// <summary>
    /// Fighter is an npc that generates slime resource.
    /// </summary>
    public class Fighter: Worker
    {
        
        public Fighter(Vector2 pos) { 
            position = pos;
            isCentered = true;
            dieAnimation = GlobalAnimations.GetAnim(AnimNames.FighterDead);
            
            animation = GlobalAnimations.GetAnim(AnimNames.FighterSlash);
            animation.isLooping = true;
            animation.shouldPlay = false;

        }

        public override void GenerateRessources()
        {
            Ressources.AddMonsterDrops(1);
        }

        public override bool TakeRessources()
        {
            return true; //Since fighter only need food and that has already been checked/eaten.
        }

    }
}
