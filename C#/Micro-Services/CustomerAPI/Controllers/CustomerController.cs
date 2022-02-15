using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository repository;

        public CustomerController(ICustomerRepository repo)
        {
            repository = repo;
        }

        // Get all Customers
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            // Call repo to get all customers
            return repository.GetAll();
        }

        // GET single Customer/1
        [Route("GetCustomer/{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            // Get customer and if it's not null, return it as a response
            var customer = repository.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            return new ObjectResult(customer);
        }

        // POST customer
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            // if customer isn't null, create a customer
            if (customer == null)
            {
                return BadRequest();
            }

            var newCustomer = repository.Add(customer);
            return CreatedAtRoute("GetCustomer", new { id = newCustomer.ID }, newCustomer);
        }

        // PUT a customer
        [HttpPut]
        public IActionResult Edit([FromBody]Customer customer)
        {
            // If customer isn't null, update customer
            if (customer == null)
            {
                return BadRequest();
            }

            repository.Edit(customer);
            return StatusCode(204);
        }

        // DELETE customer 
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            // If an id is given, delete customer
            if (id == 0)
            {
                return BadRequest();
            }

            repository.Remove(id);
            return StatusCode(204);
        }
    }
}