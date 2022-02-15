using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComunicationLibrary
{
    public interface IStatefullComunication : IService
    {
        Task<string> GetServiceDetails();
    }
}
