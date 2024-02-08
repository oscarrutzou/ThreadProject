using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadGame
{
    /// <summary>
    /// Miner is an npc that generates money resource.
    /// It requires food to work.
    /// </summary>
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
            resourceOffSet = new Vector2(42, 23);
            workResource = new WorkResource(new Vector2(pos.X + resourceOffSet.X, pos.Y + resourceOffSet.Y), TextureNames.Crysal1, scale);
            SceneData.gameObjectsToAdd.Add(workResource);
        }


        public override void GenerateRessources()
        {
            Resources.AddMoney(addMoneyAmount);
        }

        public override bool TakeRessources()
        {
            return true; //Since fighter only need food and that has already been checked/eaten.
        }
        public override void DieAndGiveBackRessources() { } // Nothing since it dosent take extra ressources.

    }
}
