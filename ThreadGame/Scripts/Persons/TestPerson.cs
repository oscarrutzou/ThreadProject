using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadGame
{
    public class TestPerson: Person
    {

        public TestPerson(Vector2 pos) { 
            position = pos;
            isCentered = true;
            texture = GlobalTextures.textures[TextureNames.TestPerson];
        }
    }
}
