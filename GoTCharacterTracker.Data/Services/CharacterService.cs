using System;
using System.Collections.Generic;
using System.Linq;

namespace GoTCharacterTracker.Data.Characters.Services
{
    public class Character
    {
        // private members
        protected  List<CharacterDTO> m_allCharacters;

        public Character(List<CharacterDTO> names)
        {
            foreach (var name in names)
            {
                m_allCharacters.Add(name);
            }
        }


        public IEnumerable<CharacterDTO> GetCharactersByFamily(string family)
        {
            var characters = m_allCharacters.Where(c => c.Family == family);
            return characters;
        }


        public class CharacterDTO
        {
            public string Name { get; set; }
            public bool IsAlive { get; set; }
            public string Family { get; set; } // make Family an enum & maybe a class later
        }
    }
}
