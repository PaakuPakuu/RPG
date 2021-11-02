using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public abstract class Scene
    {
        protected Scene() { }

        public abstract void ExecuteScene();
    }
}
