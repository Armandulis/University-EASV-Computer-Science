using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodApi.Infrastructure;
using FoodApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _repo;

        public FoodController(IFoodRepository repo)
        {
            this._repo = repo;
        }

        // GET: api/Food
        [HttpGet]
        public IEnumerable<Food> Get()
        {
            return _repo.GetAll();
        }

        // GET: api/Food/5
        [HttpGet("{id}", Name = "Get")]
        public Food Get(int id)
        {
            if (id > 0)
            {
                return _repo.GetFood(id);
            }

            return null;
        }
        // GET: api/Food/Popcorn
        [HttpGet("{FoodName}")]
        [Route("GetByName/{name}")]
        public Food GetByName(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                return _repo.GetFoodByName(name);
            }

            return null;
        }

        // POST: api/Food
        [HttpPost]
        public IActionResult Post([FromBody] Food food)
        {
            if (food == null)
            {
                return BadRequest();
            }

            _repo.Create(food);
            return Ok();
        }

        // PUT: api/Food
        [HttpPut]
        public IActionResult Put([FromBody] Food food)
        {
            if (food == null)
            {
                return BadRequest();
            }
            _repo.Edit(food);
            return Ok();
        }

        // DELETE: api/Food/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            return Ok();
        }
    }
}
