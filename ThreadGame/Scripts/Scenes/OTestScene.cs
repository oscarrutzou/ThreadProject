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
        Random rnd = new Random();
        public override void Initialize()
        {
            Fighter t = new Fighter(new Vector2(-50, 0));

            Fighter t1 = new Fighter(new Vector2(50, 0));

            SceneData.gameObjectsToAdd.Add(t);
            SceneData.gameObjectsToAdd.Add(t1);
            
            Button b = new Button("Test", () => { }, AnimNames.MediumButtonClick, new Vector2(100, 300), 2);
            b.SetCollisionBox(65, 30);
            Button b1 = new Button("Test", 
                () => { SceneData.gameObjectsToAdd.Add(new Fighter(new Vector2(rnd.Next(200, 400), rnd.Next(200, 400)))); }, 
                AnimNames.MediumButtonClick, 
                new Vector2(300, 300), 
                2);
            b1.SetCollisionBox(65, 30);
            SceneData.gameObjectsToAdd.Add(b);
            SceneData.gameObjectsToAdd.Add(b1);
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
