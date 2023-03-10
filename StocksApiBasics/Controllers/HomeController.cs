using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;
using Microsoft.Extensions.Options;
using StocksApiBasics.Models;
using System.Text.Json;
using System.Globalization;

namespace StocksApiBasics.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly StocksApiOptions _options; //private field to create options method for StocksApiOptions.cs
        private readonly IConfiguration _config;

        public HomeController(IFinnhubService finnhubService, IOptions<StocksApiOptions> stocksApiOptions, IConfiguration configuration) //so 'stocksApiOptions' parameter get an object if IOptions
        {
            _finnhubService = finnhubService;

            _options = stocksApiOptions.Value; //and it has a predefined property called value which contains an object of stocksApiOptions class                                                    
            //stocksApiOptions is IOptions<> type, Value is 'StocksApiOptions' type
            _config= configuration;
        }



        [Route("/")]
        public async Task<IActionResult> Index()
        {
            string stockSymbol = _options.DefaultStockSymbol;

            if (string.IsNullOrEmpty(stockSymbol))
            {
                stockSymbol = "MSFT";
            }


            Dictionary<string, object> profile = await _finnhubService.GetCompanyProfile(stockSymbol); //returns json data
            Dictionary<string, object> price = await _finnhubService.GetStockPriceQuote(stockSymbol);


            StockTrade stockTrade = new StockTrade
            {
                StockSymbol = stockSymbol,
                StockName = profile.ContainsKey("name") ? profile["name"].ToString() : null,
                //StockName = (string)profile["name"]
                //Price = price.ContainsKey("c") ? price["c"].GetDouble() : 0.0
                Price = Convert.ToDouble(price["c"].ToString(), CultureInfo.InvariantCulture) //culture info is used to read decimal dot in the data
            };

            ViewBag.FinnhubToken = _config["userToken"]; //sending userToken to the view because we use it in JS file to update prices 
            return View(stockTrade);
        }
    }
}
