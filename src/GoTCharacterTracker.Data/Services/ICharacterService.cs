using System;
using System.Collections.Generic;
using GoTCharacterTracker.Data.DTO.Characters;
using GoTCharacterTracker.Data.Managers;

namespace GoTCharacterTracker.Data.Services
{
    public interface ICharacterService
    {
        IEnumerable<CharacterCardDTO> GetAllCharacters();
        CharacterCardDTO GetCharacter(int id);
        int Add(NewCharacterDTO dto);
        int Delete(int id);
        int Update(NewCharacterDTO dto, int id);
        CharacterCardDTO AssociateUserToOrganization(int personId, int organizationId);
        CharacterCardDTO AssignObjectOwnershipToPerson(int personId, int objectId);


    }
}
