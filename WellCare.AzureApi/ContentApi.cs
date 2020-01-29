using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WellCare.Core.Interface;
using WellCare.Models;

namespace WellCare.AzureApi
{
    public class ContentApi
    {
        private readonly IContentManager _manager;

        public ContentApi(IContentManager contentManager)
        {
            _manager = contentManager;
        }

        [FunctionName("GetContentById")]
        public async Task<IActionResult> GetContentById
        (
           [HttpTrigger(AuthorizationLevel.Anonymous, "get")] int Id,
           HttpRequest req,
           ILogger log
        )
        {
            log.LogInformation("C# HTTP GetHealthScore trigger function processed a request.");

            var details = await _manager.GetByIdAsync(Id);

            return new OkObjectResult(details);
        }

        [FunctionName("FilterContent")]
        public async Task<IActionResult> FilterContent
        (
           [HttpTrigger(AuthorizationLevel.Anonymous, "get")] FilterResultsRequest filter,
           HttpRequest req,
           ILogger log
        )
        {
            var details2 = new ContentDetails
            {
                Id = 1,
                AuthorId = "nsubugak@yahoo.com",
                Category = "test category",
                ContentValue = "test content",
                Title = "test title",
                Type = "test type"
            };

            var status = await _manager.SaveAsync(details2);

            log.LogInformation("C# HTTP FilterContent trigger function processed a request.");

            var details = await _manager.List(filter);

            return new OkObjectResult(details);
        }

        [FunctionName("SaveContent")]
        public async Task<IActionResult> SaveContent
        (
           [HttpTrigger(AuthorizationLevel.Anonymous, "post")] ContentDetails details,
           HttpRequest req,
           ILogger log
        )
        {
            log.LogInformation("C# HTTP FilterContent trigger function processed a request.");

            var status = await _manager.SaveAsync(details);

            return new OkObjectResult(status);
        }
    }
}
