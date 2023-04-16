using Jay.FuncHubDemo.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Jay.FuncHubDemo.Functions
{
    public class MultiOutput
    {
        [EventHubOutput(Startup.HttpHubName, Connection=Startup.HttpHubReadWriteConnection)]
        public FromHttpEvent Event { get; set; }
        public HttpResponseData HttpResponse { get; set; }
    }
}