using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadGame
{
    public class JTestScene : Scene
    {
        public override void Initialize()
        {
            
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
