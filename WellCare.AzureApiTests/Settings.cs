using System;
using System.Collections.Generic;
using System.Text;

namespace WellCare.AzureApi.IntegrationTests
{
    public class Settings
    {
        public string DotNetExecutablePath { get; set; }
        public string FunctionHostPath { get; set; }
        public string FunctionApplicationPath { get; set; }
        public string StorageConnectionString { get; set; }
    }
}
