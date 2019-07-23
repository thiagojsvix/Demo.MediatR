
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Ordering.Api.Command;
using Xunit;

namespace Ordering.IntegrationTests
{
    public class OrderTest
    {
        private string WebAppName => "Ordering.Api";
        private readonly HttpClient httpClient;
        private string testLibPath;
        private string Url => "api/Order";

        public OrderTest()
        {
            var pathRoot = this.GetAppBasePath(this.WebAppName);
            var builder = new WebHostBuilder().UseContentRoot(pathRoot).UseEnvironment("Development").UseStartup(this.WebAppName);
            var testServer = new TestServer(builder);
            this.httpClient = testServer.CreateClient();
        }

        [Fact]
        public async Task CreateOrderOk()
        {
            //arrange
            HttpResponseMessage responseMessage;
            var createOrder = new CreateOrderCommand(1, 5.25M, true);
            var jsonData = JsonConvert.SerializeObject(createOrder);

            //act
            using (var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"))
                responseMessage = await this.httpClient.PostAsync(this.Url, stringContent);
            var statusCode = responseMessage.StatusCode;

            //assert
            responseMessage.IsSuccessStatusCode.Should().BeTrue();
            statusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateOrderFaild()
        {
            //arrange
            HttpResponseMessage responseMessage;
            var createOrder = new CreateOrderCommand(0, 0M, true);
            var jsonData = JsonConvert.SerializeObject(createOrder);

            //act
            using (var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"))
                responseMessage = await this.httpClient.PostAsync(this.Url, stringContent);
            var statusCode = responseMessage.StatusCode;

            //assert
            responseMessage.IsSuccessStatusCode.Should().BeFalse();
            statusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task CreateOrderException()
        {
            //act
            var responseMessage = await this.httpClient.GetAsync($"{this.Url}/Exception");

            var content = await responseMessage.Content.ReadAsStringAsync();
            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(content);

            //asert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            responseMessage.Content.Headers.ContentType.ToString().Should().Be("application/problem+json");

            problemDetails.Status.Should().Be(500);
            problemDetails.Title.Should().Be("Throw Exception");
            problemDetails.Instance.Should().Be("/api/Order/Exception");
            problemDetails.Detail.Should().NotBeNullOrEmpty();
        }

        private string GetAppBasePath(string applicationWebSiteName)
        {
            var binPath = Environment.CurrentDirectory;
            while (!string.Equals(Path.GetFileName(binPath), "bin", StringComparison.InvariantCultureIgnoreCase))
            {
                binPath = Path.GetFullPath(Path.Combine(binPath, ".."));
                if (string.Equals(Path.GetPathRoot(binPath), binPath, StringComparison.InvariantCultureIgnoreCase))
                    throw new Exception("Could not find bin directory for test library.");
            }

            this.testLibPath = Path.GetFullPath(Path.Combine(binPath, ".."));
            var testPath = Path.GetFullPath(Path.Combine(testLibPath, ".."));
            var srcPath = Path.GetFullPath(Path.Combine(testPath, "..", "src"));

            if (!Directory.Exists(srcPath))
                throw new Exception("Could not find src directory.");

            var appBasePath = Path.Combine(srcPath, applicationWebSiteName);
            if (!Directory.Exists(appBasePath))
                throw new Exception("Could not find directory for application.");

            return appBasePath;
        }
    }
}
