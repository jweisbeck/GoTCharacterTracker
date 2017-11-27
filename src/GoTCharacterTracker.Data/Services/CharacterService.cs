using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using GoTCharacterTracker.Data.DTO.Characters;
using GoTCharacterTracker.Data.Managers;


namespace GoTCharacterTracker.Data.Services
{
    public class CharacterService: ICharacterService
    {
        // private members
        protected List<CharacterCardDTO> m_allCharacters;
        private readonly ICharacterManager m_characterManager;


        public CharacterService(ICharacterManager characterManager)
        {
            m_characterManager = characterManager;
        }

        public IEnumerable<CharacterCardDTO> GetAllCharacters() 
        {
            return m_characterManager.GetAll();       
        }

        public CharacterCardDTO GetCharacter(int id) {
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

        public CharacterCardDTO AssignObjectOwnershipToPerson(int personId, int objectId) 
        {
            return m_characterManager.AssignObjectOwnershipToPerson(personId, objectId);
        }

        public CharacterCardDTO AssociateUserToOrganization(int personId, int organizationId)
    
        {
            return m_characterManager.AssociateUserToOrganization(personId, organizationId);
        }


    }
}
