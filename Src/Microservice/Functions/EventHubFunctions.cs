using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Jay.FuncHubDemo.Models;
using Jay.FuncHubDemo.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Jay.FuncHubDemo.Functions
{
    public class EventHubFunctions
    {
        private readonly IMyService _service;
        private readonly ILogger _logger;
        public EventHubFunctions(ILoggerFactory loggerFactory, IMyService service)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<EventHubFunctions>();
        }

        [Function("EventHubAlphaConsumer")]
        [EventHubOutput(Startup.AlphaHubName, Connection=Startup.AlphaHubWriteConnection)]
        public async Task<FromAlphaConsumerPoco> RunAlphaAsync([EventHubTrigger(Startup.HttpHubName, Connection=Startup.HttpHubReadWriteConnection, ConsumerGroup= Startup.HttpHubAlphaConsumer, IsBatched = false)] FromHttpEvent @event)
        {
            _logger.LogInformation("EventHubAlphaConsumer fired with event {Event}", @event.ToString());
            return await _service.GenerateFromAlphaConsumer(@event);
        }
        
        [Function("EventHubBetaConsumer")]
        public async Task RunBetaAsync([EventHubTrigger(Startup.HttpHubName, Connection=Startup.HttpHubReadWriteConnection, ConsumerGroup= Startup.HttpHubBetaConsumer)] FromHttpEvent[] events)
        {
            //in real scenarios there would probably be a real async calls so you can lose await Task.Run(()=>{...});
            await Task.Run(() =>
            {
                try
                {
                    _logger.LogInformation("EventHubBetaConsumer fired with events count {Count}", events.Length);
                    foreach (var @event in events)
                    {
                        _logger.LogInformation("EventHubBetaConsumer fired with event {Event}", @event.ToString());
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Swallowing Exception to keep hub processing {NewLine}{Error}", Environment.NewLine, ex.ToString());
                }
            });
        }

    }
}