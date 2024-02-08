using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadGame
{
    /// <summary>
    /// Chef is an npc that generates food resource.
    /// It requires money and food to work.
    /// </summary>
    public class Chef: Worker
    {
        private int generateFoodAmount = 12;
        private int useMoneyAmount = 3;
        private int useMonsterDropAmount = 6;
        public Chef(Vector2 pos) { 
            position = pos;
            isCentered = true;
            dieAnimation = GlobalAnimations.GetAnim(AnimNames.ChefDead);
            
            animation = GlobalAnimations.GetAnim(AnimNames.Chef);
            animation.isLooping = true;
            animation.shouldPlay = false;

            resourceOffSet = new Vector2(40, 20);
            workResource = new WorkResource(new Vector2(pos.X + resourceOffSet.X, pos.Y + resourceOffSet.Y), TextureNames.ChefFood1, scale);
            SceneData.gameObjectsToAdd.Add(workResource);
        }

        public override void GenerateRessources()
        {
            Resources.AddFood(generateFoodAmount);
        }

        public override bool TakeRessources()
        {
            return Resources.UseMonsterDrops(useMonsterDropAmount) && Resources.UseMoney(useMoneyAmount);
        }

        public override void DieAndGiveBackRessources()
        {
            Resources.AddMonsterDrops(useMonsterDropAmount);
            Resources.AddMoney(useMoneyAmount);
        }
    }
}
