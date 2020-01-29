using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WellCare.Core;
using WellCare.Core.Interface;
using WellCare.Repositories;
using WellCare.Repositories.Entities;
using WellCare.Repositories.Interface;

[assembly: FunctionsStartup(typeof(WellCare.AzureApi.Startup))]
namespace WellCare.AzureApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            AutoMapperConfig.Init();

            builder.Services.AddScoped<IBaseRepository<HealthScore>, InMemoryRepository<HealthScore>>();
            builder.Services.AddScoped<IHealthScoreManager, HealthScoreManager>();
            builder.Services.AddScoped<IBaseRepository<Content>, InMemoryRepository<Content>>();
            builder.Services.AddScoped<IContentManager, ContentManager>();
        }
    }
}
