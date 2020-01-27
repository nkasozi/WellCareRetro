using Xunit;
using WellCare.AzureApi;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using WellCare.Models;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net;

namespace WellCare.AzureApi.IntegrationTests
{
    public class HealthScoreApiTests : BaseIntegrationTest
    {


        [Fact()]
        public void GetHealthScoreTest_GivenValidId_ExpectSuccess()
        {
            try
            {
                SetUp();

                using (var httpClient = new HttpClient())
                {
                    Uri url = ApiAction("GetHealthScore?Id=1");

                    var httpResponse = httpClient.GetAsync(url).Result;

                    httpResponse.EnsureSuccessStatusCode();

                    var jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;

                    var details = JsonConvert.DeserializeObject<HealthScoreDetails>(jsonResponse);

                    details.Should().NotBeNull();

                    details.status.Should().BeSameAs(Status.SUCCESS);
                }
            }
            finally
            {
                TearDown();
            }
        }

        [Fact()]
        public void GetHealthScoreTest_GivenInvalidId_ExpectFailure()
        {
            try
            {
                SetUp();

                using (var httpClient = new HttpClient())
                {
                    Uri url = ApiAction("GetHealthScore?Id=test");

                    var httpResponse = httpClient.GetAsync(url).Result;

                    httpResponse.StatusCode.Should().NotBe(HttpStatusCode.OK);
                }
            }
            finally
            {
                TearDown();
            }

        }

        [Fact()]
        public void GetHealthScoreTest_GivenNoId_ExpectFailure()
        {
            try
            {
                SetUp();

                using (var httpClient = new HttpClient())
                {
                    Uri url = ApiAction("GetHealthScore?Id=");

                    var httpResponse = httpClient.GetAsync(url).Result;

                    httpResponse.StatusCode.Should().NotBe(HttpStatusCode.OK);
                }
            }
            finally
            {
                TearDown();
            }

        }
    }
}