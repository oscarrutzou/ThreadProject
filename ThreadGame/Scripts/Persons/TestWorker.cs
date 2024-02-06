using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadGame
{
    public class TestWorker: Worker
    {

        public TestWorker(Vector2 pos) { 
            position = pos;
            isCentered = true;
            animation = GlobalAnimations.animationsTest[AnimNames.FighterDead];
            animation.isLooping = false;
        }

        public TestWorker(Vector2 pos, AnimNames name, bool loop)
        {
            position = pos;
            isCentered = true;
            animation = GlobalAnimations.animationsTest[name];
            animation.isLooping = loop;
        }
    }
}
