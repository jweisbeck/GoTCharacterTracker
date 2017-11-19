using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoTCharacterTracker.Data.Services;
using GoTCharacterTracker.Data.DTO.Characters;

namespace GoTCharacterTracker.Controllers
{
    [Route("api/GoT/[controller]")]
    public class CharactersController : Controller
    {
        private readonly ICharacterService m_characterService;

        public CharactersController(ICharacterService characterService)
        {
             m_characterService = characterService;
        }

        [HttpGet]
        public IEnumerable<CharacterDTO> Get()
        {
            return m_characterService.GetAllCharacters();
        }

        [HttpGet("{id}")]
        public CharacterDTO Get(int id)
        {
            return m_characterService.GetCharacter(id);
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
