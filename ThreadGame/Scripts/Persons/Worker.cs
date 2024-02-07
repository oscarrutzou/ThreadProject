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
                Thread.Sleep(rnd.Next(1000, 3000));

                if (!Ressources.GetFood(1) || !TakeRessources())
                {
                    WorkerDie();
                    break;
                }

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
            animation.onAnimationDone -= ResetAnimWork;
        }

        internal void WorkerDie()
        {
            animation = dieAnimation;
            animation.shouldPlay = true;

            //Other stuff here like making the worker float up and turn alpha down.

            Thread.Sleep(3000);

            cts.Cancel();
            isRemoved = true;
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
        public abstract void GenerateRessources();

    }
}
