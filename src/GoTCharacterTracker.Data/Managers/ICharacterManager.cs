using System;
using System.Collections.Generic;
using GoTCharacterTracker.Data.DTO.Characters;

namespace GoTCharacterTracker.Data.Managers
{
    public interface ICharacterManager
    {
        int Add(NewCharacterDTO character);
        IEnumerable<CharacterDTO> GetAll();
        CharacterDTO GetByID(int id);

    }
}
