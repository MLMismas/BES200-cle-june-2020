using LibraryApi.Controllers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class GettingStatusTests : IClassFixture<WebTestFixture>
    {
        private HttpClient Client;

        public GettingStatusTests(WebTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetTheStatus()
        {
            var response = await Client.GetAsync("/status");
            response.EnsureSuccessStatusCode();
            Assert.True(response.IsSuccessStatusCode);

            var content = await response.Content.ReadAsAsync<GetStatusResponse>();

            Assert.Equal("Everything is golden! ", content.Message);
            Assert.Equal("Joe Schmidtly", content.CheckedBy);
            Assert.Equal(new DateTime(1969, 4, 20, 23, 59, 00), content.WhenLastChecked);
        }
    }
}
