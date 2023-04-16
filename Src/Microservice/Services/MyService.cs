using System;
using System.Threading.Tasks;
using Jay.FuncHubDemo.Models;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Jay.FuncHubDemo.Services
{
    public interface IMyService
    {
        Task<FromHttpEvent> GenerateFromHttpEvent(HttpRequestData request);
        Task<FromAlphaConsumerPoco> GenerateFromAlphaConsumer(FromHttpEvent @event);
    }
    internal class MyService: IMyService
    {
        private readonly ILogger _logger;
        public MyService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MyService>();
        }
        public async Task<FromHttpEvent> GenerateFromHttpEvent(HttpRequestData request)
        { 
            //in real scenarios there would probably be a real async calls so you can lose await Task.Run(()=>{...});
            return await Task.Run(() =>
            {
                _logger.LogDebug("GenerateFromHttpEvent Called");
                return new FromHttpEvent
                {
                    Date = DateTimeOffset.Now,
                    Name = $"func:{request.FunctionContext.FunctionId} inv: {request.FunctionContext.InvocationId}",
                    IsSomething = true
                };
            });
        }

        public async Task<FromAlphaConsumerPoco> GenerateFromAlphaConsumer(FromHttpEvent @event)
        { 
            //in real scenarios there would probably be a real async calls so you can lose await Task.Run(()=>{...});
            return await Task.Run(() =>
            {
                _logger.LogDebug("GenerateFromAlphaConsumer Called");
                return new FromAlphaConsumerPoco
                {
                    Original = @event,
                    ProcessedOn = DateTimeOffset.Now
                };
            });
        }
    }
}