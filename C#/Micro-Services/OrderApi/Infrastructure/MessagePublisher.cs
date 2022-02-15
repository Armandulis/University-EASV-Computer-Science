using EasyNetQ;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure
{
    public class MessagePublisher : IMessagePublisher, IDisposable
    {
        IBus bus;
        public MessagePublisher(string cloudAMQPConnectionString)
        {
            bus = RabbitHutch.CreateBus(cloudAMQPConnectionString);
        }
        public void Dispose()
        {
            bus.Dispose();
        }
        void IMessagePublisher.PublishOrderStatusChangedMessage(int? customerId, IEnumerable<OrderLine> orderLines, string topic)
        {
            var message = new OrderStatusChangedMessage
            {
                CustomerId = customerId,
                //OrderLines = orderLines
            };

            bus.Publish(message, topic);
        }
        Customer IMessagePublisher.RequestCustomer(int id)
        {
            var cr = new CustomerRequest { ID = id };
            var response = bus.Request<CustomerRequest, Customer>(cr);
            return response;
        }
    }
}
