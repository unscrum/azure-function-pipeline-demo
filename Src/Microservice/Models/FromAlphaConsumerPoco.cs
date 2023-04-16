using System;

namespace Jay.FuncHubDemo.Models
{
    public class FromAlphaConsumerPoco
    {
        public FromHttpEvent Original { get; set; }
        public DateTimeOffset ProcessedOn { get; set; }
    }
}