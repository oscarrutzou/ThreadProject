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
        internal Vector2 ressourceOffSet = new Vector2(50, 35);

        internal bool isWorking;
        internal Animation dieAnimation;
        public bool isDying;
        internal Random rnd = new Random();
        internal Thread workThread; //handles the behavior cycle of the worker, it contains a custom update method.
        internal Thread lifeThread; //handles the lifespan of the worker, it's used as a glorified timer.
        internal int workTimeInSec = 3;
        private int lifeInSec = 30;
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

                if (!Ressources.GetFood(1))
                {
                    WorkerDie();
                    DieAndGiveBackRessources();
                    break;
                }

                Thread.Sleep(rnd.Next(1000, 3000));

                //Begin working:
                isWorking = true;
                animation.shouldPlay = true;
                Thread.Sleep(workTimeInSec *  1000);

                //Work done:
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
        /// Called before the worker starts working.
        /// Takes a set amount of resources from a common resource pool.
        /// </summary>
        /// <returns>true if a resource was taken.</returns>
        public abstract bool TakeRessources();
       
        /// <summary>
        /// Should give back the same amount that it took in TakeRessources.
        /// </summary>
        public abstract void DieAndGiveBackRessources();

        /// <summary>
        /// Called after a worker has successfully finished a work cycle.
        /// Adds a set amount of resources to a common resource pool.
        /// </summary>
        public abstract void GenerateRessources();

    }
}
