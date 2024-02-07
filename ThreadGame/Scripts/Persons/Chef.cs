using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadGame
{
    public class Chef: Worker
    {
        private int generateFoodAmount = 4;
        private int useMonsterDropAmount = 1;
        public Chef(Vector2 pos) { 
            position = pos;
            isCentered = true;
            dieAnimation = GlobalAnimations.GetAnim(AnimNames.ChefDead);
            
            animation = GlobalAnimations.GetAnim(AnimNames.Chef);
            animation.isLooping = true;
            animation.shouldPlay = false;

            ressourceOffSet = new Vector2(55, 20);
            workRessource = new WorkRessource(new Vector2(pos.X + ressourceOffSet.X, pos.Y + ressourceOffSet.Y), TextureNames.ChefFood1, scale);
            SceneData.gameObjectsToAdd.Add(workRessource);
        }

        public override void GenerateRessources()
        {
            Ressources.AddFood(generateFoodAmount);
        }

        public override bool TakeRessources()
        {
            return Ressources.GetMonsterDrop(useMonsterDropAmount);
        }

        public override void DieAndGiveBackRessources()
        {
            Ressources.AddMonsterDrops(useMonsterDropAmount);
        }
    }
}
