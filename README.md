# Stock Price App v2

#### This is a simple web application that displays the stock price of a company in real-time. Additionally, it has methods a simple interface for managing stock orders. This project was developed as part of a learning exercise to gain experience with .NET Core, C#, and web API development.

## Overview

#### The application uses the MVC pattern, dependency injection, the Options pattern, and services to display the stock price of Microsoft Corp. Firstly, a service file was added which contains the logic to retrieve data from the Finnhub API service in JSON format. The app is connected to the API with a unique token which is stored in User Secrets to make it inaccessible from outside. The token value is retrieved in the service class by using the IConfiguration interface's reference. The application also has an additional class to assign the value of Stock Symbol which is stored in appsettings.json. The IOptions pattern is used here to assign the property of the additional class to store the value from appsettings.json. We can fetch data of any company by using its unique Stock Symbol. The methods of services are called from the Controller class by passing the Stock Symbol of the company. After getting data from the API, specific ones are selected and assigned to the properties of the model class which has properties "StockSymbol", "StockName", "Price" and "Quantity". In the Controller, the values of those properties are assigned and passed to the view. In the View, the "Strongly Typed View" approach is used. There is also a JavaScript file in the view to connect to the Finnhub WebSocket and update the stock price in real-time.

## Additional functionalities

#### The Stocks Service provides the following functionality:

* Create a new buy order
* Get all buy orders
* Get a buy order by ID
* Update a buy order
* Delete a buy order

The service also includes validation of request data, including checks for required fields and range validation on numeric fields.



## Technologies

###
* C#
* ASP.NET Core
* MVC pattern
* Dependency Injection
* Options pattern
* Razor syntax
* JavaScript
* WebSocket
* xUnit
* Custom Validation


## Conclusion
#### In conclusion, building the Stock Price App has allowed me to gain hands-on experience with C#, ASP.NET Core, and the MVC pattern. I have learned how to leverage dependency injection and the Options pattern to store and retrieve application settings securely. I have also gained proficiency in using Razor syntax to generate HTML views and JavaScript to enable real-time updates using WebSocket. Additionally, I have learned how to integrate third-party APIs, such as the Finnhub API, to retrieve data and display it in the application. Moreover, I have implemented Stock Order interface to handle orders. I acquired a knowledge of DTOs (Data Transfer Objects) and which cases they should be implemented. Also, I gain an understanding of xUnit tests and why they are important in any project. These skills and techniques are essential for building modern web applications that are scalable, maintainable, and efficient. Overall, this project has provided me with valuable knowledge and practical experience that I can apply to future web development projects.











