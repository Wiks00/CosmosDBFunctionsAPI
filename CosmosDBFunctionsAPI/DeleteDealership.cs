using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using APIArtifacts;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace CosmosDBFunctionsAPI
{
    public static class DeleteDealership
    {
        [FunctionName("DeleteDealership")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "Delete", Route = "{id}")] HttpRequestMessage req,
            [DocumentDB("Audit", "DIff", Id = "{id}", ConnectionStringSetting = "CosmosDBConnectionString")] dbObject oldDealership,
            [DocumentDB("Audit", "DIff", ConnectionStringSetting = "CosmosDBConnectionString")] out dbObject newDealership,
            TraceWriter log)
        {
            
            if (oldDealership.EndDate != null)
            {
                newDealership = null;
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            oldDealership.EndDate = DateTimeOffset.UtcNow;

            newDealership = new dbObject
            {
                dealership = oldDealership.dealership,
                dealershipFacility = oldDealership.dealershipFacility,
                dealershipThirdParty = oldDealership.dealershipThirdParty,
                departments = oldDealership.departments,

                createdOn = oldDealership.createdOn,
                UserId = Guid.Parse(req.Headers.GetValues("x-ms-client-principal-id").First()),

                EndDate = DateTimeOffset.UtcNow
            };

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
