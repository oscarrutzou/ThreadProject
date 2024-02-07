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
        internal WorkRessource workRessource;
        internal Vector2 ressourceOffSet = new Vector2(50, 35);

        internal bool isWorking;
        internal Animation dieAnimation;
        internal Random rnd = new Random();
        internal Thread workThread;
        internal Thread lifeThread;
        internal int workTimeInSec = 3;
        private int lifeInSec = 30;
        private CancellationTokenSource cts = new CancellationTokenSource();
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

        public virtual void OwnUpdate(CancellationToken token)
        {
            while (!token.IsCancellationRequested && !isRemoved)
            {

                if (!TakeRessources()) continue;

                if (!Ressources.GetFood(1))
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

        internal void WorkerDie()
        {
            animation = dieAnimation;
            animation.shouldPlay = true;
            animation.frameRate = 5f;

            //Other stuff here like making the worker float up and turn alpha down.

            Thread.Sleep(3000);

            cts.Cancel();
            isRemoved = true;

            if (workRessource != null) 
                workRessource.isRemoved = true;
        }

        private void LifeCycle()
        {
            Thread.Sleep(lifeInSec * 1000);
            while (isWorking) //Wait until isWorking is false
            {
                Thread.Sleep(100); //Sleep for a short time to prevent busy waiting
            }

            WorkerDie();
        }

        public abstract bool TakeRessources();
        /// <summary>
        /// Should give back the same amount that it took in TakeRessources.
        /// </summary>
        /// <returns></returns>
        public abstract void DieAndGiveBackRessources();
        public abstract void GenerateRessources();

    }
}
