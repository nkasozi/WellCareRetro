using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using WellCare.AzureApi.IntegrationTests;
using WellCare.Models;
using Xunit;

namespace WellCare.AzureApiTests1
{
    [Collection(nameof(TestCollection))]
    public class ContentApiTests
    {
        private TestFixture _fixture;

        public ContentApiTests(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact()]
        public void FilterContent_GivenValidFilterResultsRequest_ExpectSuccess()
        {
            //save something first
            SaveContent_GivenValidContentDetails_ExpectSuccess();

            //now we can proceed
            string url2 = "api/FilterContent?Term=test";

            var httpResponse2 = _fixture.Client.GetAsync(url2).Result;

            httpResponse2.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonResponse = httpResponse2.Content.ReadAsStringAsync().Result;

            var details = JsonConvert.DeserializeObject<List<ContentListItem>>(jsonResponse);

            details.Should().NotBeNull();
            details.Should().NotBeEmpty();
        }


        [Fact()]
        public void SaveContent_GivenValidContentDetails_ExpectSuccess()
        {
            string url = "api/SaveContent";

            var details = new ContentDetails
            {
                Id = 1,
                AuthorId = "nsubugak@yahoo.com",
                Category = "test category",
                ContentValue = "test content",
                Title = "test title",
                Type = "test type"
            };

            var json = JsonConvert.SerializeObject(details);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = _fixture.Client.PostAsync(url,  content).Result;

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;

            var status = JsonConvert.DeserializeObject<Status>(jsonResponse);

            status.Should().NotBeNull();
            status.StatusCode.Should().Be(Status.SUCCESS_STATUS_CODE);
            status.StatusDesc.Should().Be(Status.SUCCESS_STATUS_TEXT);
        }

        [Fact()]
        public void SaveContent_GivenInvalidContentDetails_ExpectFailure()
        {
            string url = "api/SaveContent";

            var details = new ContentDetails
            {

            };

            var json = JsonConvert.SerializeObject(details);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponse = _fixture.Client.PostAsync(url, content).Result;

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;

            var status = JsonConvert.DeserializeObject<Status>(jsonResponse);

            status.Should().NotBeNull();
            status.StatusCode.Should().NotBe(Status.SUCCESS_STATUS_CODE);
            status.StatusDesc.Should().NotBe(Status.SUCCESS_STATUS_TEXT);
        }



    }
}
