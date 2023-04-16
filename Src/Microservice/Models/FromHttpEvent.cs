using System;

namespace Jay.FuncHubDemo.Models
{
    public class FromHttpEvent
    {
        public string Name { get; set; }
        public bool IsSomething { get; set; }
        public DateTimeOffset Date { get; set; }
        public override string ToString()
        {
            return $"{Name}|{IsSomething}|{Date:s}";
        }
    }
}
