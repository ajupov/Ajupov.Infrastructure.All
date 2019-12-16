namespace Ajupov.Infrastructure.All.Tracing.Settings
{
    public class TracingSettings
    {
        public string AgentHost { get; set; }

        public int AgentPort { get; set; }

        public double SamplingRate { get; set; }
        
        public double LowerBound { get; set; }
    }
}