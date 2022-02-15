using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeAPI.Repository;

namespace PrimeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrimeController : ControllerBase
    {
        Random random = new Random();
        int isDown;

        private PrimesRepo _pr;
        public PrimeController()
        {
            _pr = new PrimesRepo();

        }

        // GET api/values/12
        [HttpGet("{number}")]
        public ActionResult<bool> Get(int number)
        {
            // One out of 4 times the server will be down
            // 0 means the server is down.
            // Else means the server is up.
            isDown = random.Next(3);

            if (isDown == 0)
                return NotFound();
            else
                return Ok(_pr.IsPrime(number));
        }

        // GET api/values/12
        [HttpPost("{number}")]
        public ActionResult<int> Post(int number)
        {
            // One out of 4 times the server will be down
            // 0 means the server is down.
            // Else means the server is up.
            isDown = random.Next(3);

            if (isDown == 0)
                return NotFound();
            else
                return Ok(_pr.GetPrimesSum(number));
        }

    }
}