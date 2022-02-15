using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Data;
using ComunicationLibrary;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace ProductServiceApi
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class ProductServiceApi : StatefulService, IStatefullComunication
    {
        public ProductServiceApi(StatefulServiceContext context)
            : base(context)
        { }

        public async Task<string> GetServiceDetails()
        {
            var serviceName = this.Context.ServiceName.ToString();
            var partitionId = this.Context.PartitionId.ToString();

            var replicaId = this.Context.ReplicaId.ToString();

            return $"{serviceName} ::: {partitionId} ::: {replicaId}";
        }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return this.CreateServiceRemotingReplicaListeners();
        }
    }
}
