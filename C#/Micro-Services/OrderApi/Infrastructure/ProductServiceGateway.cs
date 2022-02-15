using RestSharp;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure
{
    public class ProductServiceGateway : IServiceGateway<Product>
    {
        Uri productServiceBaseUrl;
        public ProductServiceGateway(Uri baseUrl)
        {
            productServiceBaseUrl = baseUrl;
        }
        public Product Get(int ID)
        {
            RestClient c = new RestClient();
            c.BaseUrl = productServiceBaseUrl;

            var request = new RestRequest(ID.ToString(), Method.GET);
            var response = c.Execute<Product>(request);
            var orderedProduct = response.Data;
            return orderedProduct;
        }
    }
}