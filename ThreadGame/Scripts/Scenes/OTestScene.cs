using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace ThreadGame
{
    public class OTestScene : Scene
    {
        public override void Initialize()
        {
            TestWorker t = new TestWorker(new Vector2(-50, 0), AnimNames.FighterSlash, true);

            TestWorker t1 = new TestWorker(new Vector2(50, 0), AnimNames.FighterSlash, true);

            SceneData.gameObjectsToAdd.Add(t);
            SceneData.gameObjectsToAdd.Add(t1);
        }


        public override void DrawOnScreen()
        {
            base.DrawOnScreen();

            if (InputManager.debugStats) DebugVariables.DrawDebug();

        }

        public override void Update()
        {
            base.Update();
            

        }
    }
}
