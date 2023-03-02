using ServiceContracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Net;


namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public FinnhubService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }


        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {

            //get api token from configuration which is in user secret
            string token = _configuration["userToken"]; // we want to get user token from user secret file inside StocksApiProject.

            //we create a new HttpClient instance using the IHttpClientFactory
            HttpClient httpClient = _httpClientFactory.CreateClient();

            //building apiurl with 'stockSymbol' and 'token'
            string apiUrl = $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={token}";

            //we send an HTTP GET request to the API endpoint
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            //we read the response body and store it into string
            string responseBody = await response.Content.ReadAsStringAsync();

            //we parse json data into a dictionary
            Dictionary<string, object> jsonCompany = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            return jsonCompany;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            string token = _configuration["userToken"];

            HttpClient httpClient = _httpClientFactory.CreateClient();

            string apiUrl = $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={token}";

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            string responseBody = await response.Content.ReadAsStringAsync();

            Dictionary<string, object> jsonPrice = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            return jsonPrice;

        }
    }
}