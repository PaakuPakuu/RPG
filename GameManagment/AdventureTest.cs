using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public sealed class AdventureTest : Adventure
    {
        public AdventureTest() : base()
        {
            _mapList.Add(new Map("MainTown"));
        }
    }
}
