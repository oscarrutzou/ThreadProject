using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace ThreadGame
{
    public abstract class Person : GameObject
    {
        #region Variables
        //Time for action
        //Other variables
        #endregion

        public override void Update()
        {
            //Behøver ikke base.Update da der ik sker noget i Update i gameobject.
        }

        public override void Draw()
        {
            base.Draw(); //Skal have den her base.Draw ellers tegner den ikke
        }
    }
}
