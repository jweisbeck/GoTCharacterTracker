using System;
using System.Collections.Generic;
using System.Linq;
using GoTCharacterTracker.Data.DTO.Characters;
using GoTCharacterTracker.Data.Managers;


namespace GoTCharacterTracker.Data.Services
{
    public class CharacterService
    {
        // private members
        protected List<CharacterDTO> m_allCharacters;
        private readonly CharacterManager m_characterManager;


        public CharacterService()
        {
            m_characterManager = new CharacterManager();
        }

        public IEnumerable<CharacterDTO> GetAllCharacters() 
        {
            return m_characterManager.GetAll();       
        }

        public CharacterDTO GetCharacter(int id) {
            return m_characterManager.GetByID(id);
        }

    }
}
