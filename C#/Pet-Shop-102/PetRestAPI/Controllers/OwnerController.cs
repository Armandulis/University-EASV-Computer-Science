using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.ApplicationService.Services;
using PetShop.Core.Entity;

namespace PetRestAPI.Controllers
{
    [Produces("application/json")]
    [Route("/[controller]")]
    public class OwnerController : ControllerBase
    {

        readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService OwnerService)
        {
            _ownerService = OwnerService;
        }
        // GET api/Pets
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Owner>> Get()
        {
            return _ownerService.ReadAllOwners();
        }

        // GET api/Pets/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            return _ownerService.FindOwnderByID(id);
        }

        // POST api/Pets
        [Authorize]
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner owner)
        {
            if (owner.FirstName == null)
            {
                return BadRequest("Owner's name is required!");
            }
            else { return _ownerService.NewOwner(owner); }
        }

        // PUT api/Pets/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Owner owner)
        {
            _ownerService.UpdateOwner(id, owner.FirstName, owner.LastName, owner.Address, owner.PhoneNumber, owner.Email );
        }

        // DELETE api/Pets/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _ownerService.DeleteOwner(id);
        }
    }
}