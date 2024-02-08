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
        internal WorkResource workResource;
        internal Vector2 resourceOffSet = Vector2.Zero;

        internal bool isWorking;
        internal Animation dieAnimation;
        public bool isDying;
        internal Random rnd = new Random();
        public static int foodEatAmount = 3;
        internal int workTimeInSec = 3;
        private int lifeInSec = 60;

        internal Thread workThread; //handles the behavior cycle of the worker, it contains a custom update method.
        internal Thread lifeThread; //handles the lifespan of the worker, it's used as a glorified timer.
        //Cancellation token is used to clear thread memory when the thread is disposed.
        public CancellationTokenSource cts = new CancellationTokenSource();    
        #endregion

        protected Worker()
        {
            workThread = new Thread(() => OwnUpdate());
            workThread.IsBackground = true;
            workThread.Start();

            lifeThread = new Thread(LifeCycle);
            lifeThread.IsBackground = true;
            lifeThread.Start();

 
            layerDepth = 0.4f;
        }

        /// <summary>
        /// OwnUpdate is a worker's independent update method.
        /// It updates seperately from GameWorld.Update
        /// </summary>
        private void OwnUpdate()
        {
            while (!cts.Token.IsCancellationRequested && !isRemoved) 
            {
                Thread.Sleep(rnd.Next(1000, 3000)); //So there is some room between each time
                if (!TakeRessources()) continue; //Can be e.g monsterdrops or money

                if (!Resources.UseFood(foodEatAmount)) //Every worker needs to eat when starting a action
                {
                    WorkerDie(); //Kill worker, and remove it after some time
                    DieAndGiveBackRessources(); //If the worker dies, give back the previous taken ressources
                    break;
                }

                //Begin working:
                isWorking = true;
                animation.shouldPlay = true;
                Thread.Sleep(workTimeInSec *  1000);

                //Work done:
                animation.onAnimationDone += ResetAnimWork;
                GenerateRessources();
                isWorking = false;
            }

            // Check if cancellation is requested, then die with the death animation
            if (cts.Token.IsCancellationRequested) WorkerDie();
        }

        private void ResetAnimWork()
        {
            animation.shouldPlay = false;
            animation.PauseAnim();
            animation.onAnimationDone -= ResetAnimWork;
        }

        /// <summary>
        /// WorkerDie is called when the worker's LifeCycle method finishes or there aren't enough food.
        /// Begins the death animation and removes the worker shortly after.
        /// </summary>
        internal void WorkerDie()
        {
            animation = dieAnimation;
            animation.shouldPlay = true;
            animation.frameRate = 5f; //Die animation should have a slower fps.
            isDying = true; //For the methods in the UIOverlay.

            //Possible to put extra stuff here, like making the worker float up and turn alpha down.
            Thread.Sleep(3000);
            cts.Cancel();
            isRemoved = true; //Removes it from the world

            if (workResource != null) 
                workResource.isRemoved = true; //Removes the ressource
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
