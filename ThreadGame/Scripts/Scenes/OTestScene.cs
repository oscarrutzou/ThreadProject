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
        private UIOverlay overlay = new UIOverlay();
        public override void Initialize()
        {
            overlay.InitUI();

        }

        public override void Update()
        {
            base.Update();
            
            overlay.Update();
        }
    }
}
