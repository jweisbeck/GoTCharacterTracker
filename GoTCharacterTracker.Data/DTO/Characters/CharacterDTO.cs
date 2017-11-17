using System;
using System.Collections.Generic;
using System.Linq;

namespace GoTCharacterTracker.Data.Characters.DTO
{
    public class CharacterDTO
    {
        public string Name { get; set; }
        public bool IsAlive { get; set; }
        public string Family { get; set; } // make Family an enum & maybe a class later
    }
}
