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
    /// It requires food to work.
    /// </summary>
    public class Fighter: Worker
    {
        private int addDropAmount = 9;
        private int costInMoney = 3;
        public Fighter(Vector2 pos) { 
            position = pos;
            isCentered = true;
            dieAnimation = GlobalAnimations.GetAnim(AnimNames.FighterDead);
            
            animation = GlobalAnimations.GetAnim(AnimNames.FighterSlash);
            animation.isLooping = true;
            animation.shouldPlay = false;

            resourceOffSet = new Vector2(50, 10);
            workResource = new WorkResource(new Vector2(pos.X + resourceOffSet.X, pos.Y + resourceOffSet.Y), AnimNames.SlimeIdle, scale);
            workResource.animation.isLooping = true;
            workResource.animation.shouldPlay = true;
            SceneData.gameObjectsToAdd.Add(workResource);
        }

        public override bool TakeRessources()
        {
            return Resources.UseMoney(costInMoney);
        }

        public override void DieAndGiveBackRessources()
        {
            Resources.AddMoney(costInMoney);
        }
        public override void GenerateRessources()
        {
            Resources.AddMonsterDrops(addDropAmount);
        }
    }
}
