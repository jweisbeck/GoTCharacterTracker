using System;
using System.Collections.Generic;
using GoTCharacterTracker.Data.DTO.Characters;
using GoTCharacterTracker.Data.Managers;

namespace GoTCharacterTracker.Data.Services
{
    public interface ICharacterService
    {
        IEnumerable<CharacterDTO> GetAllCharacters();
        CharacterDTO GetCharacter(int id);
        int Add(NewCharacterDTO dto);
    }
}
