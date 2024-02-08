using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace ThreadGame
{
    /// <summary>
    /// A worker provides certain resources, whilst requiring other resources to stay alive.
    /// </summary>
    public abstract class Worker : GameObject
    {

        #region Variables
        //Time for action
        //Other variables
        internal WorkRessource workRessource;
        internal Vector2 ressourceOffSet = Vector2.Zero;

        internal bool isWorking;
        internal Animation dieAnimation;
        public bool isDying;
        internal Random rnd = new Random();
        internal Thread workThread; //handles the behavior cycle of the worker, it contains a custom update method.
        internal Thread lifeThread; //handles the lifespan of the worker, it's used as a glorified timer.
        public static int foodEatAmount = 3;
        internal int workTimeInSec = 3;
        private int lifeInSec = 60;
        private CancellationTokenSource cts = new CancellationTokenSource();    //Cancellation token is used to clear thread memory when the thread is disposed.
        #endregion

        protected Worker()
        {
            layerDepth = 0.4f;
            workThread = new Thread(() => OwnUpdate(cts.Token));
            workThread.IsBackground = true;
            workThread.Start();

            lifeThread = new Thread(LifeCycle);
            lifeThread.IsBackground = true;
            lifeThread.Start();

 
        }

        /// <summary>
        /// OwnUpdate is a worker's independent update method.
        /// It updates seperately from GameWorld.Update
        /// </summary>
        /// <param name="token"></param>
        public virtual void OwnUpdate(CancellationToken token)
        {
            while (!token.IsCancellationRequested && !isRemoved) //Quit loop when this GameObject is marked as removed
            {

                if (!TakeRessources()) continue;

                if (!Ressources.UseFood(foodEatAmount))
                {
                    WorkerDie();
                    DieAndGiveBackRessources();
                    break;
                }

                Thread.Sleep(rnd.Next(1000, 3000));

                isWorking = true;
                animation.shouldPlay = true;
                Thread.Sleep(workTimeInSec *  1000);
                animation.onAnimationDone += ResetAnimWork;

                GenerateRessources();
                isWorking = false;
            }
        }

        private void ResetAnimWork()
        {
            animation.shouldPlay = false;
            animation.PauseAnim();
            animation.onAnimationDone -= ResetAnimWork;
        }

        /// <summary>
        /// WorkerDie is called when the worker's LifeCycle method finishes.
        /// Begins the death animation and removes the worker shortly after.
        /// </summary>
        internal void WorkerDie()
        {
            animation = dieAnimation;
            animation.shouldPlay = true;
            animation.frameRate = 5f;
            isDying = true;
            //Other stuff here like making the worker float up and turn alpha down.

            Thread.Sleep(3000);

            cts.Cancel();
            isRemoved = true;

            if (workRessource != null) 
                workRessource.isRemoved = true;
        }

        /// <summary>
        /// LifeCycle functions as a timer that kills the worker upon its end.
        /// A worker cannot die while it is working.
        /// </summary>
        private void LifeCycle()
        {
            Thread.Sleep(lifeInSec * 1000);
            while (isWorking) //Wait until isWorking is false
            {
                Thread.Sleep(100); //Sleep for a short time to prevent busy waiting
            }

            WorkerDie();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool TakeRessources();
        /// <summary>
        /// Should give back the same amount that it took in TakeRessources.
        /// </summary>
        /// <returns></returns>
        public abstract void DieAndGiveBackRessources();
        public abstract void GenerateRessources();

    }
}
