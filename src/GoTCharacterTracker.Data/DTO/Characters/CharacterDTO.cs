using System;
using System.Collections.Generic;
using System.Linq;

namespace GoTCharacterTracker.Data.DTO.Characters
{
    public class CharacterDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsAlive { get; set; }
        public HouseDTO House { get; set; }
    }
}
