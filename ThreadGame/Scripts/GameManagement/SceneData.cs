using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadGame
{
    public static class SceneData
    {
        //All gameObjects in the scene
        public static List<GameObject> gameObjects = new List<GameObject>();
        public static List<GameObject> gameObjectsToAdd = new List<GameObject>();


        public static List<Gui> guis = new List<Gui>();
        public static List<Worker> workers = new List<Worker>();
        public static List<GameObject> defaults = new List<GameObject>();
    }
}
