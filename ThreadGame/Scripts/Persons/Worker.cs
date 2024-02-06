using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace ThreadGame
{
    public abstract class Worker : GameObject
    {

        #region Variables
        //Time for action
        //Other variables
        public bool isAlive = true;
        Thread thread;
        public static int food = 5;
        public static int money;
        static readonly object foodLock = new object();

        #endregion
        protected Worker()
        {
            thread = new Thread(OwnUpdate);
            thread.IsBackground = true;
            thread.Start();
            
        }


        //public override void Update()
        //{
        //    //Behøver ikke base.Update da der ik sker noget i Update i gameobject.
        //}
        public virtual void OwnUpdate()
        {
            while (isAlive)
            {
                lock (foodLock)
                {
                    if (food > 0)
                    {
                        food--;
                    }
                    else
                    {
                        //Die
                        isRemoved = true;
                        isAlive = false;
                    }
                }

                if (!isAlive) break;
                //Start work
                Thread.Sleep(1000);
                //Give ressources
                money++;

            }
        }


        public override void Draw()
        {
            base.Draw(); //Skal have den her base.Draw ellers tegner den ikke
        }
    }
}
