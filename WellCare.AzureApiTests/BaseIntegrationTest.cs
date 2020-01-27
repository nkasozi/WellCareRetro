using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace WellCare.AzureApi.IntegrationTests
{
    public class BaseIntegrationTest
    {
        private IDisposable _webApp;
        private static int _hostPort = 9443;

        protected Uri ApiAction(string actionName)
        {
            return new Uri($"http://localhost:{_hostPort}/api/{actionName}");
        }

        public void SetUp()
        {
            AutoMapperConfig.Init();
            _webApp = WebApp.Start<Startup>(string.Format("http://*:{0}/", _hostPort));
        }


        public void TearDown()
        {
            _webApp.Dispose();
        }
    }
}
