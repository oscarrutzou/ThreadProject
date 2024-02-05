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
            TestPerson t = new TestPerson(new Vector2(-50, 0), AnimNames.FighterSlash, true);

            TestPerson t1 = new TestPerson(new Vector2(50, 0), AnimNames.FighterSlash, true);

            SceneData.gameObjectsToAdd.Add(t);
            SceneData.gameObjectsToAdd.Add(t1);

            t.animation.onAnimationDone += ChangeToDeadAnimForTestning;
        }

        private void ChangeToDeadAnimForTestning()
        {
            if (SceneData.persons.Count > 0) {
                SceneData.persons.Last().animation = GlobalAnimations.animationsTest[AnimNames.FighterDead];
            }
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
