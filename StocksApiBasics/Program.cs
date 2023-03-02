using StocksApiBasics;
using ServiceContracts;
using Services; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<StocksApiOptions>(builder.Configuration.GetSection("TradingOptions")); //we are adding configuration as a service with option type StocksApiOptions 
builder.Services.AddSingleton<IFinnhubService, FinnhubService>(); //initializing interface and class in IoC
builder.Services.AddHttpClient();


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();   

app.Run();
