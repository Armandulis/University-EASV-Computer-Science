using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishOrderStatusChangedMessage(int? customerId, IEnumerable<OrderLine> orderLines, string topic);
        Customer RequestCustomer(int id);
    }
}
