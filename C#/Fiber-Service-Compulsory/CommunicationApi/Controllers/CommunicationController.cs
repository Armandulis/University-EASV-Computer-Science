using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComunicationLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace CommunicationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunicationController : ControllerBase
    {
        [HttpGet]
        [Route("statefull")]
        public async Task<String> StatefullGet([FromQuery] string region)
        {
            var statefullProxy = ServiceProxy.Create<IStatefullComunication>(
                new Uri("fabric:/ServiceFabricStatefull/ProductServiceApi"),
                new Microsoft.ServiceFabric.Services.Client.ServicePartitionKey(region.ToUpperInvariant()));

            var serviceName = await statefullProxy.GetServiceDetails();

            return serviceName;
        }
    }
}
