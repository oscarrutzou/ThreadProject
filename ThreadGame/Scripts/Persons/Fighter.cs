﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadGame
{
    public class Fighter: Worker
    {
        
        public Fighter(Vector2 pos) { 
            position = pos;
            isCentered = true;
            dieAnimation = GlobalAnimations.GetAnim(AnimNames.FighterDead);
            
            animation = GlobalAnimations.GetAnim(AnimNames.FighterSlash);
            animation.isLooping = true;
            animation.shouldPlay = false;

            ressourceOffSet = new Vector2(65, 10);
            workRessource = new WorkRessource(new Vector2(pos.X + ressourceOffSet.X, pos.Y + ressourceOffSet.Y), AnimNames.SlimeIdle, scale);
            workRessource.animation.isLooping = true;
            workRessource.animation.shouldPlay = true;
            SceneData.gameObjectsToAdd.Add(workRessource);
        }


        public override void GenerateRessources()
        {
            Ressources.AddMonsterDrops(1);
        }

        public override bool TakeRessources()
        {
            return true; //Since fighter only need food and that has already been checked/eaten.
        }
        public override void DieAndGiveBackRessources() { } // Nothing since it dosent take extra ressources.

    }
}
