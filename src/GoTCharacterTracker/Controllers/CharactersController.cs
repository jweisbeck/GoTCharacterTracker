using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoTCharacterTracker.Data.Services;
using GoTCharacterTracker.Data.DTO.Characters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace GoTCharacterTracker.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/GoT/characters")]
    public class CharactersController : Controller
    {
        private readonly ICharacterService m_characterService;

        public CharactersController(ICharacterService characterService)
        {
             m_characterService = characterService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var characters = m_characterService.GetAllCharacters();

            return new ObjectResult(characters);
        }

        [AllowAnonymous]
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
            var rows = m_characterService.Add(dto);

            if(rows == 0) {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]NewCharacterDTO dto)
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

        [HttpPost("{personId}/organization/{orgId}")]
        public IActionResult UpdateOrganizations(int personId, int orgId)
        {

            var character = m_characterService.AssociateUserToOrganization(personId, orgId);

            if (character == null)
            {
                return NotFound();
            }

            return new ObjectResult(character);
        }


        [HttpPut("{personId}/objects/{objectId}")]
        public IActionResult UpdateObjects(int personId, int objectId)
        {

            var character = m_characterService.AssignObjectOwnershipToPerson(personId, objectId);

            if (character == null)
            {
                return NotFound();
            }

            return new ObjectResult(character);
        }

    }
}
