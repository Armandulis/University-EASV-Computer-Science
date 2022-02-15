using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaAPI.Infrastructure;
using CinemaAPI.Model;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace CinemaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaRepository _repo;

        public CinemaController(ICinemaRepository repo)
        {
            this._repo = repo;
        }

        // GET: api/Cinema
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _repo.GetAll();
        }

        // GET: api/Cinema/5
        [HttpGet("{id}", Name = "Get")]
        public Order Get(int id)
        {
            if (id > 0)
            {
                return _repo.Get(id);
            }

            return null;
        }

        // POST: api/Cinema
        [HttpPost]
        public Order Post([FromBody] Order order)
        {
            if (order == null)
            {
                return null;
            }

            // Call ProductApi to get the food ordered
            RestClient c = new RestClient();
            //desktop-661danr
            c.BaseUrl = new Uri("http://desktop-661danr:19081/SFCinema/TicketsApi/api/tickets/getbytitle");
            var requestTicket = new RestRequest(order.Movie.ToString(), Method.GET);

            var responseTicket = c.Execute<Ticket>(requestTicket);
            var orderedTicket = responseTicket.Data;

            if (order.TicketQuantity <= orderedTicket.TicketsLeft)
            {
                orderedTicket.TicketsLeft = orderedTicket.TicketsLeft - order.TicketQuantity;

                c.BaseUrl = new Uri("http://desktop-661danr:19081/SFCinema/TicketsApi/api/tickets");
                var updateTicketRequest = new RestRequest("", Method.PUT);
                updateTicketRequest.AddJsonBody(orderedTicket);
                var updateTicketResponse = c.Execute(updateTicketRequest);

                if (updateTicketResponse.IsSuccessful)
                {
                    order.Price = order.TicketQuantity * orderedTicket.Price;

                    c.BaseUrl = new Uri("http://desktop-661danr:19081/SFCinema/FoodApi/api/food/getbyname");
                    var requestFood = new RestRequest(order.Food.ToString(), Method.GET);

                    var responseFood = c.Execute<Food>(requestFood);
                    var orderedFood = responseFood.Data;

                    if (order.FoodQuantity <= orderedFood.AmountLeft)
                    {
                        orderedFood.AmountLeft = orderedFood.AmountLeft - order.FoodQuantity;

                        c.BaseUrl = new Uri("http://desktop-661danr:19081/SFCinema/FoodApi/api/food");
                        var updateFoodRequest = new RestRequest("", Method.PUT);
                        updateFoodRequest.AddJsonBody(orderedFood);
                        var updateFoodResponse = c.Execute(updateFoodRequest);

                        if (updateFoodResponse.IsSuccessful)
                        {
                            order.Price = order.Price + order.FoodQuantity * orderedFood.Price;
                            return _repo.Add(order);
                        }
                    }

                    return _repo.Add(order);
                }
            }
            return null;
        }

        // PUT: api/Cinema/5
        [HttpPut]
        public IActionResult Put([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            _repo.Edit(order);
            return Ok();
        }

        // DELETE: api/Cinema/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.Remove(id);
            return Ok();
        }
    }
}
