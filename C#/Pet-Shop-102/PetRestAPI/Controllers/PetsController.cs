using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.AplicationService;
using PetShop.Core.Entity;

namespace PetRestAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        readonly IPetService _petService;

        public PetsController(IPetService petService) {
            _petService = petService;

        }
        // GET api/Pets
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get()
        {
            return _petService.GetAllPets();
        }

        // GET api/Pets/5
        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)
        {
            return _petService.FindPetByID(id);
        }

        // POST api/Pets
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            if (pet.Name == null)
            {
                return BadRequest("Pet's name is required!");
            }
            else { return _petService.CreatePet(pet); }
        }

        // PUT api/Pets/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Pet pet)
        {
            _petService.UpdatePet(id, pet.Name,pet.Type,pet.Price);
        }

        // DELETE api/Pets/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _petService.RemovePet(id);
        }
    }
}
