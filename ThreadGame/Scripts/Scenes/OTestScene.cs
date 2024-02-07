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
            Fighter f = new Fighter(new Vector2(-50, 0));

            //Fighter f1 = new Fighter(new Vector2(50, 0));
            Miner m = new Miner(new Vector2(100, 0));

            SceneData.gameObjectsToAdd.Add(f);
            //SceneData.gameObjectsToAdd.Add(f1);
            SceneData.gameObjectsToAdd.Add(m);

            Button b = new Button("Test", () => { Ressources.AddFood(5); }, AnimNames.MediumButtonClick, new Vector2(100, 300), 2);
            //b.SetCollisionBox(65, 30);
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
