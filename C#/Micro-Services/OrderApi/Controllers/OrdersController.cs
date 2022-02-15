using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderApi.Data;
using OrderApi.Infrastructure;
using RestSharp;
using SharedModels;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<Order> repository;
        readonly IServiceGateway<Product> productServiceGateway;
        readonly IMessagePublisher messagePublisher;
        public OrdersController(IRepository<Order> repoes, IServiceGateway<Product> gateway, IMessagePublisher publisher)
        {
            repository = repoes;
            productServiceGateway = gateway;
            messagePublisher = publisher;
        }

        // GET: api/orders
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return repository.GetAll();
        }

        // GET api/orders/5
        [HttpGet]
        [Route("getById/{id}", Name = "getById")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet]
        [Route("getByCustomerId/{customerId}", Name = "getByCustomerId")]
        public IEnumerable<Order> GetOrderById(int customerId)
        {
            return repository.GetAllByCustomer(customerId);
        }

        [HttpGet]
        [Route("getByProductId/{productId}")]
        public IActionResult GetProductById(int productId)
        {
            var ord = new Order();
            var orderline = new OrderLine { ProductId = 1, OrderId = 1, Id = 1, Quantity = 1 };

            ord.OrderLines = new OrderLine[] { orderline };
            if (ProductItemsAvailable(ord))
            {
                return StatusCode(200, "Connection worked");
            }
            return StatusCode(200, "Connection worked");
        }

        [HttpPut]
        [Route("cancelOrder/{orderId}")]
        public IActionResult CancelOrder(int orderId)
        {
            try
            {
                Order selectedOrder = repository.Get(orderId);

                if (selectedOrder.StatusCode != Order.Status.Shipped)
                {
                    selectedOrder.StatusCode = Order.Status.Cancelled;
                    repository.Edit(selectedOrder);

                    return Ok();
                }
                else
                {
                    return BadRequest("Order could not be cancelled");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException != null ? ex.Message + ex.InnerException : ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            
           if(ProductItemsAvailable(order))
                try
                {
                    // Publish OrderStatusChangedMessage. If this operation
                    // fails, the order will not be created
                    messagePublisher.PublishOrderStatusChangedMessage(
                      order.CustomerID, order.OrderLines, "completed");

                    // Create order.
                    order.StatusCode = Order.Status.Completed;
                    //var newOrder = repository.Add(order);
                    return CreatedAtRoute("getById", new { id = order.Id }, order);
                }
                catch
                {
                    return StatusCode(500, "Please try again an error occured.");
                }
            else
            {
                return StatusCode(500, "Item is out of stock.");
            }

        }


        private bool ProductItemsAvailable(Order order)
        {
            foreach (var orderLine in order.OrderLines)
            {
                // Call product service to get the product ordered.
                var orderedProduct = productServiceGateway.Get(orderLine.ProductId);
                if (orderLine.Quantity > orderedProduct.ItemsInStock - orderedProduct.ItemsReserved)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
