using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using APIArtifacts;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace CosmosDBFunctionsAPI
{
    public static class CreateDealership
    {
        [FunctionName("CreateDealership")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)]HttpRequestMessage req,
            [DocumentDB("Audit", "DIff", ConnectionStringSetting = "CosmosDBConnectionString", SqlQuery = "SELECT VALUE c.dealership.dealershipDetail.lookersId FROM c WHERE c.EndDate != 'null'")] IEnumerable<string> uniqueItems,
            [DocumentDB("Audit", "DIff", ConnectionStringSetting = "CosmosDBConnectionString")] out dbObject newDealership,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            newDealership = req.Content.ReadAsAsync<dbObject>().Result;

            //ToDo: Unique DealerName
            if(uniqueItems.Contains(newDealership.dealership.dealershipDetail.lookersId))
            {
                newDealership = null;
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            newDealership.UserId = Guid.Parse(req.Headers.GetValues("x-ms-client-principal-id").First());

            return req.CreateResponse(HttpStatusCode.Created, newDealership.id);
        }
    }
}
