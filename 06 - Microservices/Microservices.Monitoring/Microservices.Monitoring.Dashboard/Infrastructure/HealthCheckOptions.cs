namespace Microservices.Monitoring.Dashboard.Infrastructure
{
    public class HealthCheckOptions
    {
        public string HeaderText { get; set; }
        public int PollingInterval { get; set; }
        public string UIPath { get; set; }
        public string ApiPath { get; set; }
    }
}