using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Prime.Function;

public class IsPrime
{
    private readonly ILogger<IsPrime> _logger;

    public IsPrime(ILogger<IsPrime> logger)
    {
        _logger = logger;
    }

    [Function("IsPrime")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        int num = int.Parse(req.Query["number"]!);
        if ((num & 1) == 0) {
            return new OkObjectResult(false);
        }

        for(int i = 3; i < Math.Sqrt(num); i += 1)
        {
            if (num % i == 0)
                return new OkObjectResult(false);
        }

        return new OkObjectResult(true);
    }
}