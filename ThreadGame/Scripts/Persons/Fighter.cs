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

            ressourceOffSet = new Vector2(50, 10);
            workRessource = new WorkRessource(new Vector2(pos.X + ressourceOffSet.X, pos.Y + ressourceOffSet.Y), AnimNames.SlimeIdle, scale);
            workRessource.animation.isLooping = true;
            workRessource.animation.shouldPlay = true;
            SceneData.gameObjectsToAdd.Add(workRessource);
        }

        public override void GenerateRessources()
        {
            Ressources.AddMonsterDrops(addDropAmount);
        }

        public override bool TakeRessources()
        {
            return Ressources.UseMoney(costInMoney);
        }

        public override void DieAndGiveBackRessources()
        {
            Ressources.AddMoney(costInMoney);
        }

    }
}
