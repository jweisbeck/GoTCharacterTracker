using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoTCharacterTracker.Data.Services;
using GoTCharacterTracker.Data.DTO.Characters;


namespace GoTCharacterTracker.Api.Controllers
{
    [Route("api/GoT/characters")]
    public class CharactersController : Controller
    {
        private readonly ICharacterService m_characterService;

        public CharactersController(ICharacterService characterService)
        {
             m_characterService = characterService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var characters = m_characterService.GetAllCharacters();

            return new ObjectResult(characters);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var character = m_characterService.GetCharacter(id);

            if (character == null)
            {
                return NotFound();
            }

            return new ObjectResult(character);
        }

        [HttpPost]
        public IActionResult Post([FromBody]NewCharacterDTO dto)
        {
            if(dto == null) {
                return BadRequest();
            }
            var added = m_characterService.Add(dto);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] NewCharacterDTO dto)
        {

            if (dto == null)
            {
                return BadRequest();
            }

            var rows = m_characterService.Update(dto, id);

            if(rows == 0) {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            
            var rows = m_characterService.Delete(id);

            if (rows == 0)
            {
                return NotFound();
            }

            return Ok();

        }
    }
}
