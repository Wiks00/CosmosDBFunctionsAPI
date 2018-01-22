using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using APIArtifacts;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace CosmosDBFunctionsAPI
{
    public static class GetDealerships
    {
        [FunctionName("GetDealerships")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{id}")]HttpRequestMessage req, 
            string id, 
            Binder binder,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            DocumentDBAttribute docAttr;
            object dbObjects;

            if (id != null)
            {
                docAttr = new DocumentDBAttribute("Audit", "DIff")
                {
                    ConnectionStringSetting = "CosmosDBConnectionString",
                    Id = "{id}"
                };

                dbObjects = await binder.BindAsync<dbObject>(docAttr);
            }
            else
            {
                docAttr = new DocumentDBAttribute("Audit", "DIff")
                {
                    ConnectionStringSetting = "CosmosDBConnectionString",
                    SqlQuery = "SELECT * FROM c WHERE c.EndDate != 'null'"
                };

                dbObjects = await binder.BindAsync<IEnumerable<dbObject>>(docAttr);
            }

            return req.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(dbObjects));
        }
    }
}
