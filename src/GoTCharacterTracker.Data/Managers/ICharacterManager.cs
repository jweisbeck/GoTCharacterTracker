using System;
using System.Collections.Generic;
using GoTCharacterTracker.Data.DTO.Characters;

namespace GoTCharacterTracker.Data.Managers
{
    public interface ICharacterManager
    {
        int Add(NewCharacterDTO character);
        IEnumerable<CharacterCardDTO> GetAll();
        CharacterCardDTO GetByID(int id);
        int Delete(int id);
        int Update(NewCharacterDTO dto, int id);


    }
}
