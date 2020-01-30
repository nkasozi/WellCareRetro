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
using System.Net.Http.Formatting;

namespace WellCare.AzureApi.IntegrationTests
{
    [Collection(nameof(TestCollection))]
    public class HealthScoreApiTests
    {
        private readonly TestFixture _fixture;

        public HealthScoreApiTests(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact()]
        public void SaveHealthScoreTest_GivenValidHealthScore_ExpectSuccess()
        {
            string url = "api/SaveHealthScore";

            var healthScore = new HealthScoreDetails
            {
                UserId = "nsubugak@yahoo.com",
                BloodPressure = "1",
                Weight = 150,
                Id = 1
            };

            var json = JsonConvert.SerializeObject(healthScore);
            
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = _fixture.Client.PostAsync(url, content).Result;

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;

            var details = JsonConvert.DeserializeObject<Status>(jsonResponse);

            details.Should().NotBeNull();
            details.StatusCode.Should().Be(Status.SUCCESS_STATUS_CODE);
            details.StatusDesc.Should().Be(Status.SUCCESS_STATUS_TEXT);
        }

        [Fact()]
        public void GetHealthScoreTest_GivenValidId_ExpectSuccess()
        {
            SaveHealthScoreTest_GivenValidHealthScore_ExpectSuccess();

            string url = "api/GetHealthScoreById?Id=1";

            var httpResponse = _fixture.Client.GetAsync(url).Result;

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;

            var details = JsonConvert.DeserializeObject<HealthScoreDetails>(jsonResponse);

            details.Should().NotBeNull();
            details.status.StatusCode.Should().Be(Status.SUCCESS_STATUS_CODE);
            details.status.StatusDesc.Should().Be(Status.SUCCESS_STATUS_TEXT);
        }

        [Fact()]
        public void GetHealthScoreTest_GivenNonexistentID_ExpectFailure()
        {
            string url = "api/GetHealthScoreById?Id=1000";

            var httpResponse = _fixture.Client.GetAsync(url).Result;

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;

            var details = JsonConvert.DeserializeObject<HealthScoreDetails>(jsonResponse);

            details.Should().NotBeNull();

            details.status.StatusCode.Should().NotBe(Status.SUCCESS_STATUS_CODE);
            details.status.StatusDesc.Should().NotBe(Status.SUCCESS_STATUS_TEXT);
        }

        [Fact()]
        public void GetHealthScoreTest_GivenInvalidId_ExpectFailure()
        {
            string url = "api/GetHealthScoreById?Id=test";

            var httpResponse = _fixture.Client.GetAsync(url).Result;

            httpResponse.StatusCode.Should().NotBe(HttpStatusCode.OK);
        }

        [Fact()]
        public void GetHealthScoreTest_GivenNoId_ExpectFailure()
        {
            string url = "api/GetHealthScoreById?Id=";

            var httpResponse = _fixture.Client.GetAsync(url).Result;

            httpResponse.StatusCode.Should().NotBe(HttpStatusCode.OK);
        }
    }
}