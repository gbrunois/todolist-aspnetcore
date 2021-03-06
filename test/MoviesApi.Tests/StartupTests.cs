using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Web.MoviesApi.Repositories;
using Xunit;

namespace Web.MoviesApi.Tests
{
    public class StartupTests
    {
        private readonly string _ContentRoot = Path.Combine(Directory.GetCurrentDirectory(), "../../src/MoviesApi");

        public StartupTests()
        {

        }

        [Fact]
        public async void Should_Serve_StaticFiles()
        {

            // Arrange
            var _server = new TestServer(
                new WebHostBuilder()
                .UseContentRoot(_ContentRoot)
                .UseStartup<Startup>());
            var _client = _server.CreateClient();

            // Act
            var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), "index.html");
            var responseMessage = await _server.CreateClient().SendAsync(requestMessage);

            // Assert
            Assert.Equal(responseMessage.StatusCode, HttpStatusCode.OK);
        }

        [Fact]
        public void Should_Resolve_IMoviesRepository()
        {
            // Arrange
            var env = new Microsoft.AspNetCore.Hosting.Internal.HostingEnvironment();
            env.ContentRootPath = Path.Combine(_ContentRoot);
            
            Startup startup = new Startup(env);
            var serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);

            // Act
            IMoviesRepository moviesRepository = (IMoviesRepository)serviceCollection.BuildServiceProvider().GetService(typeof(IMoviesRepository));

            // Assert
            Assert.NotNull(moviesRepository);
        }
    }
}