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
            TestPerson t = new TestPerson(new Vector2(0, 0));
            TestPerson t1 = new TestPerson(new Vector2(50, 0));

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
