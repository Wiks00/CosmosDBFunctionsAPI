using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using APIArtifacts;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace CosmosDBFunctionsAPI
{
    public static class UpdateDealership
    {
        [FunctionName("UpdateDealership")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "{id}")]HttpRequestMessage req,
            [DocumentDB("Audit", "DIff", Id = "{id}", ConnectionStringSetting = "CosmosDBConnectionString")] dbObject oldDealership,
            [DocumentDB("Audit", "DIff", ConnectionStringSetting = "CosmosDBConnectionString")] out dbObject newDealership,
            TraceWriter log)
        {
            if (oldDealership.EndDate != null)
            {
                newDealership = null;
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            newDealership = req.Content.ReadAsAsync<dbObject>().Result;

            oldDealership.EndDate = DateTimeOffset.UtcNow;

            newDealership.createdOn = oldDealership.createdOn;
            newDealership.UserId = Guid.Parse(req.Headers.GetValues("x-ms-client-principal-id").First());

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
