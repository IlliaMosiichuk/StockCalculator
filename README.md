# Stock Calculator

How to run:
------------
Prerequisites: 
- .NET 6
- Visual Studio 2022 (optional)

Using Visual Studio:
1. Open this solution in a Visual Studio
2. Open file appsettings.json and replace a value of "AlphaVantageApiKey" with provided in email api key and value of "DefaultConnection" with your local MS SQL Server connection string
- If you don't have MS SQL Server locally then uncomment "UseInMemoryDatabase" to use in memory database
3. Set WebAPI as Startup project 
4. Run the project
5. Go to url https://localhost:7168/swagger/index.html to explore swagger

Using command line:
1. Open file appsettings.json and replace a value of "AlphaVantageApiKey" with provided in email api key and value of "DefaultConnection" with your local MS SQL Server connection string
- If you don't have MS SQL Server locally then uncomment "UseInMemoryDatabase" to use in memory database
2. Open a solution folder in the command line
3. Run a command "dotnet run --project ./WebAPI/WebAPI.csproj"
4. Go to url https://localhost:7168/swagger/index.html to explore swagger

Possible improvements:
------------
- Add unit tests
- More structured exception handling: add generic error response model, implement a middleware to handle exceptions globally, create custom exceptions (e.g. for external stock data API), return different status codes
- Setup background task to cleanup previous weeks data (Can be used: IHostedService or Hangfire to perform background tasks inside of WebAPI process, separate worker service or some cloud technologies, e.g. Azure Functions)
- In case when app is intended to have only single endpoint then we can use "Minimal APIs" to get rid of unnecessary complexity
- In case when app is intended to have a lot of endpoints, we can use Mediator pattern inside of controllers to reduce coupling between controllers and dependencies
- Use more lightweight databases instead of MS SQL
- Add table constraints to avoid data inconsistency
- Handle possible case when as a result of parallel requests for same symbol, duplicated prices are inserted to database
- Add retry when external stock data service is failing (e.g. use Polly library)
