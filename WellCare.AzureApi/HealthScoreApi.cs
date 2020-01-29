using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WellCare.Core;
using WellCare.Models;
using WellCare.Repositories.Entities;
using WellCare.Core.Interface;

namespace WellCare.AzureApi
{
    public class HealthScoreApi
    {

        private readonly IHealthScoreManager _manager;

        public HealthScoreApi(IHealthScoreManager manager)
        {
            _manager = manager;
        }

        [FunctionName("GetHealthScoreById")]
        public async Task<IActionResult> GetHealthScoreById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP GetHealthScore trigger function processed a request.");

            string Id = req.Query["Id"];

            if (string.IsNullOrEmpty(Id))
                return new BadRequestObjectResult
                (
                    "Please pass an Id on the query " +
                    "string or in the request body"
                );

            if (!int.TryParse(Id, out _))
                return new BadRequestObjectResult
                (
                    "Id must be an int"
                );

            var details = await _manager.GetByIdAsync(int.Parse(Id));

            return new OkObjectResult(details);
        }

        [FunctionName("SaveHealthScore")]
        public async Task<IActionResult> SaveHealthScore(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation($"C# HTTP {nameof(SaveHealthScore)} trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var details = JsonConvert.DeserializeObject<HealthScoreDetails>(requestBody);

            var result = await _manager.SaveAsync(details);

            return new OkObjectResult(result);
        }
    }
}
