using System;
using System.Collections.Generic;
using System.Linq;

namespace GoTCharacterTracker.Data.DTO.Characters
{
    public class CharacterDTO
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public bool isAlive { get; set; }
        public int familyId { get; set; } // make Family its own DTO to select more family fields in the future
    }
}
