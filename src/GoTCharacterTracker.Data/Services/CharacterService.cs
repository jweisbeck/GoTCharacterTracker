using System;
using System.Collections.Generic;
using System.Linq;
using GoTCharacterTracker.Data.DTO.Characters;
using GoTCharacterTracker.Data.Managers;


namespace GoTCharacterTracker.Data.Services
{
    public class CharacterService: ICharacterService
    {
        // private members
        protected List<CharacterDTO> m_allCharacters;
        private readonly ICharacterManager m_characterManager;


        public CharacterService(ICharacterManager characterManager)
        {
            m_characterManager = characterManager;
        }

        public IEnumerable<CharacterDTO> GetAllCharacters() 
        {
            return m_characterManager.GetAll();       
        }

        public CharacterDTO GetCharacter(int id) {
            return m_characterManager.GetByID(id);
        }

        public int Add(NewCharacterDTO dto)
        {
            return m_characterManager.Add(dto);
        }

        public int Delete(int id)
        {
            return m_characterManager.Delete(id);
        }

        public int Update(NewCharacterDTO dto, int id)
        {
            return m_characterManager.Update(dto, id);
        }

    }
}
