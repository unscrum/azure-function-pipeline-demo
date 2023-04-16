using System;
using System.Net;
using System.Threading.Tasks;
using Jay.FuncHubDemo.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Jay.FuncHubDemo.Functions
{
    public class HttpFunctions
    {
        private readonly IMyService _myService;
        private readonly ILogger _logger;
        public HttpFunctions(ILoggerFactory loggerFactory, IMyService myService)
        {
            _myService = myService;
            _logger = loggerFactory.CreateLogger<HttpFunctions>();
        }

        [Function("HttpPostDemo")]
        public async Task<MultiOutput> RunPostAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Demo")]
            HttpRequestData request)
        {
            _logger.LogInformation("HttpDemo Triggered with request {Url} and {Method}", request.Url, request.Method);
            return new MultiOutput
            {
                Event = await _myService.GenerateFromHttpEvent(request),
                HttpResponse = request.CreateResponse(HttpStatusCode.Created)
            };
        }
        
        [Function("HttpGetDemo")]
        public async Task<HttpResponseData> RunGetAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Demo")]
            HttpRequestData request)
        {
            _logger.LogInformation("HttpDemo Triggered with request {Url} and {Method}", request.Url, request.Method);
            var response = request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            await response.WriteStringAsync("Welcome to Azure Functions 5.0 with Event Hub Demo!");

            return response;
        }
    }
}