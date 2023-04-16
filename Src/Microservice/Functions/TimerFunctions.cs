using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Jay.FuncHubDemo.Functions
{
    public class TimerFunctions
    {
        private readonly ILogger _logger;
        public TimerFunctions(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TimerFunctions>();
        }

        [Function("TimerDemo")]
        public async Task RunAsync([TimerTrigger("0 0/10 * * * *")] TimerInfo trigger)
        {
            //in real scenarios there would probably be a real async calls so you can lose await Task.Run(()=>{...});
            await Task.Run(() =>
            {
                _logger.LogInformation("Trigger Fired for TimerDemo with schedule next:{Next} last:{Last} lastUpdated:{LastUpdated}",
                    trigger.ScheduleStatus?.Next, trigger.ScheduleStatus?.Last,  trigger.ScheduleStatus?.LastUpdated );
                if (trigger.IsPastDue)
                {
                    _logger.LogWarning("Trigger was past due");
                }
            });
        }
    }
}