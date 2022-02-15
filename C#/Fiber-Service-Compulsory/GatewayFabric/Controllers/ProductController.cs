using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Fabric;
using System.Net.Http;

namespace GatewayFabric.Controllers
{
    [Route("Gateway/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ConfigSettings configSettings;
        private readonly StatelessServiceContext serviceContext;
        private readonly HttpClient httpClient;
        private readonly FabricClient fabricClient;


        public ProductController(ConfigSettings configSettings, StatelessServiceContext serviceContext, HttpClient httpClient, FabricClient fabricClient)
        {
            this.serviceContext = serviceContext;
            this.configSettings = configSettings;
            this.httpClient = httpClient;
            this.fabricClient = fabricClient;
        }


        public async Task<IActionResult> GetAsync()
        {
            try
            {
                string productServicesfuri = this.serviceContext.CodePackageActivationContext.ApplicationName + "/" + this.configSettings.ProductServiceName;

                string proxyUrl =
                      $"http://localhost:{this.configSettings.ReverseProxyPort}/{productServicesfuri.Replace("fabric:/", "")}/products";
                HttpResponseMessage response = await this.httpClient.GetAsync(proxyUrl);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return this.StatusCode((int)response.StatusCode);
                }

                return this.Ok(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}