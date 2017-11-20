using System;
using System.Collections.Generic;
using System.Linq;

namespace GoTCharacterTracker.Data.DTO.Characters
{
    public class NewCharacterDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsAlive { get; set; }
        public int houseId { get; set; }
    }
}
