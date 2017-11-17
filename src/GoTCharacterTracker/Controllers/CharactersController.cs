using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoTCharacterTracker.Data.Managers;
using GoTCharacterTracker.Data.DTO.Characters;

namespace GoTCharacterTracker.Controllers
{
    [Route("api/[controller]")]
    public class CharactersController : Controller
    {

        private readonly CharacterManager m_characterManager;

        public CharactersController(){
            m_characterManager = new CharacterManager();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<CharacterDTO> Get()
        {
             var characters = m_characterManager.GetAll();
            return characters;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public CharacterDTO Get(int id)
        {
            var characters = m_characterManager.GetByID(1);
            return characters;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
