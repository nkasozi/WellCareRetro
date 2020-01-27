using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WellCare.AzureApi.IntegrationTests
{
    [CollectionDefinition(nameof(TestCollection))]
    public class TestCollection : ICollectionFixture<TestFixture>
    {
    }
}
