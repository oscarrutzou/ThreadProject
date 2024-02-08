using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadGame
{
    public class Miner: Worker
    {
        private int addMoneyAmount = 9;
        public Miner(Vector2 pos) { 
            position = pos;
            isCentered = true;
            dieAnimation = GlobalAnimations.GetAnim(AnimNames.MinerDead);
            
            animation = GlobalAnimations.GetAnim(AnimNames.Miner);
            animation.isLooping = true;
            animation.shouldPlay = false;
            ressourceOffSet = new Vector2(42, 23);
            workRessource = new WorkRessource(new Vector2(pos.X + ressourceOffSet.X, pos.Y + ressourceOffSet.Y), TextureNames.Crysal1, scale);
            SceneData.gameObjectsToAdd.Add(workRessource);
        }


        public override void GenerateRessources()
        {
            Ressources.AddMoney(addMoneyAmount);
        }

        public override bool TakeRessources()
        {
            return true; //Since fighter only need food and that has already been checked/eaten.
        }
        public override void DieAndGiveBackRessources() { } // Nothing since it dosent take extra ressources.

    }
}
