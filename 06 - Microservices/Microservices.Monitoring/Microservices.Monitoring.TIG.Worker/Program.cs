using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microservices.Monitoring.TIG.Worker
{
    class Program
    {
        private const string HealthCheckUrl = "https://localhost:44337/hc";
        private const string InfluxdbWriteUrl = "http://localhost:8086/write?db=telegraf";
        private const string WebHostName = "gpf1";
        static async Task Main(string[] args)
        {
            var statuses = await GetHealthStatus();
            await PostToInfluxDb(statuses);
        }

        private static async Task<List<HealthCheckResult>> GetHealthStatus()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(HealthCheckUrl);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<HealthCheckResult>>(json);
            }
        }

        private static async Task PostToInfluxDb(List<HealthCheckResult> statuses)
        {
            foreach (var status in statuses)
            {
                var body = $"health,host={WebHostName},service={status.Service} value={status.Status}";
                using (var content = new StringContent(body))
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(InfluxdbWriteUrl, content);
                }
            }
        }

    }
}
