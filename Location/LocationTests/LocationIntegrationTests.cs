using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LocationTests
{
    public class LocationIntegrationTests
    {
        static int RecordId = 0;

        [Fact]
        public async void DeleteRecordNotExistShouldReturnNotFound()
        {
            var factory = new WebApplicationFactory<LocationAPI.Startup>();

            // Create an HttpClient which is setup for the test host
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("api/location/5");

            // Assert
            Assert.Equal((int)response.StatusCode, StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task AddNewLocationShouldReturn201()
        {
            var factory = new WebApplicationFactory<LocationAPI.Startup>();

            // Create an HttpClient which is setup for the test host
            var client = factory.CreateClient();


            var request = new
            {
                Url = "api/locations",
                Body = new
                {
                    cityId = "1",
                    latitude = 31.945368,
                    longitude = 35.928371,
                    description = "this is test to see how it's going to work",
                    buildingNumber = "14",
                    floorNumber = "1",
                }
            };

            var json = JsonConvert.SerializeObject(request.Body, Formatting.Indented);

            var stringContent = new StringContent(json, Encoding.Default, "application/json");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.PostAsync(request.Url, stringContent);

            RecordId = int.Parse(await response.Content.ReadAsStringAsync());


            // Assert
            Assert.Equal((int)response.StatusCode, StatusCodes.Status201Created);
        }

        [Fact]
        public async void DeleteRecordShouldReturnOk()
        {
            await AddNewLocationShouldReturn201();

            var factory = new WebApplicationFactory<LocationAPI.Startup>();

            // Create an HttpClient which is setup for the test host
            var client = factory.CreateClient();

            // Act
            var response = await client.DeleteAsync($"api/locations/{RecordId}");

            // Assert
            Assert.Equal((int)response.StatusCode, StatusCodes.Status200OK);
        }

        [Fact]
        public async Task UpdateLocationShouldReturnOk()
        {
            var factory = new WebApplicationFactory<LocationAPI.Startup>();

            // Create an HttpClient which is setup for the test host
            var client = factory.CreateClient();

            await AddNewLocationShouldReturn201();

            var request = new
            {
                Url = $"api/locations/{RecordId}",
                Body = new
                {
                    id = RecordId,
                    cityId = "1",
                    latitude = 31.945368,
                    longitude = 35.928371,
                    description = "this is test to see how it's going to work",
                    buildingNumber = "15",
                    floorNumber = "15",
                }
            };

            var json = JsonConvert.SerializeObject(request.Body, Formatting.Indented);

            var stringContent = new StringContent(json, Encoding.Default, "application/json");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.PutAsync(request.Url, stringContent);

            // Assert
            Assert.Equal((int)response.StatusCode, StatusCodes.Status200OK);
        }

        [Fact]
        public async Task UpdateRecordNotExistLocationShouldReturnNotFound()
        {
            var factory = new WebApplicationFactory<LocationAPI.Startup>();

            // Create an HttpClient which is setup for the test host
            var client = factory.CreateClient();

            var request = new
            {
                Url = $"api/locations/{RecordId}",
                Body = new
                {
                    id = RecordId,
                    cityId = "1",
                    latitude = 31.945368,
                    longitude = 35.928371,
                    description = "this is test to see how it's going to work",
                    buildingNumber = "15",
                    floorNumber = "15",
                }
            };

            var json = JsonConvert.SerializeObject(request.Body, Formatting.Indented);

            var stringContent = new StringContent(json, Encoding.Default, "application/json");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await client.PutAsync(request.Url, stringContent);

            // Assert
            Assert.Equal((int)response.StatusCode, StatusCodes.Status404NotFound);
        }

    }
}
