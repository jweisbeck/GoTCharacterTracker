using System;
using System.Collections.Generic;
using GoTCharacterTracker.Data.DTO.Characters;

namespace GoTCharacterTracker.Data.Managers
{
    public interface ICharacterManager
    {
        void Add(CharacterDTO character, int houseId);
        IEnumerable<CharacterDTO> GetAll();
        CharacterDTO GetByID(int id);

    }
}
