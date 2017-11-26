using System;
using System.Collections.Generic;
using System.Linq;
using GoTCharacterTracker.Data.DTO.Family;
using GoTCharacterTracker.Data.DTO.Organization;
using GoTCharacterTracker.Data.DTO.Possesions;

namespace GoTCharacterTracker.Data.DTO.Characters
{
    public class CharacterCardDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsAlive { get; set; }
        public HouseDTO House { get; set; }
        public OrganizationDTO Organizations { get; set; }
        public ObjectDTO Objects { get; set; }
    }
}
