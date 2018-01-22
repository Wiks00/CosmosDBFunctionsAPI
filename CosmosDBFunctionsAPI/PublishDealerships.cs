using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using APIArtifacts;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace CosmosDBFunctionsAPI
{
    public static class PublishDealerships
    {
        [FunctionName("PublishDealerships")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req,
            [DocumentDB("Audit", "DIff",ConnectionStringSetting = "CosmosDBConnectionString", SqlQuery = "SELECT * FROM c WHERE c.EndDate != 'null'")] IEnumerable<dbObject> dbCollection,
            [ServiceBus("dealerships", AccessRights.Manage, Connection = "ServiceBusConnectionString")] ICollector<string> sbCollection,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            foreach (var dealership in dbCollection)
            {
                sbCollection.Add(JsonConvert.SerializeObject(dealership));
            }

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
