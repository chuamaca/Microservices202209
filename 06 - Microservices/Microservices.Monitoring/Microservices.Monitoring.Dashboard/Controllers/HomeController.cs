using HealthChecks.UI.Core.Data;
using Microservices.Monitoring.Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;
using Steeltoe.Discovery.Eureka;
using System;
using System.Diagnostics;

namespace Microservices.Monitoring.Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HealthChecksDb _db;
        private readonly IDiscoveryClient _discoveryClient;
        private readonly EurekaDiscoveryManager _discoveryManager;

        public HomeController(ILogger<HomeController> logger, 
            HealthChecksDb healthChecksDb, 
            IDiscoveryClient discoveryClient,
            EurekaDiscoveryManager discoveryManager
            )
        {
            _logger = logger;
            _db = healthChecksDb;
            _discoveryClient = discoveryClient;
            _discoveryManager = discoveryManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddService(string uri,string name)
        {
            var client = _discoveryManager.Client;
            client.Applications.GetRegisteredApplications().ToList().ForEach(application =>
            {
                application.Instances.ToList().ForEach(instance =>
                {

                    var configuration = _db.Configurations.FirstOrDefault(c => c.Name == instance.InstanceId);

                    if (configuration == null)
                    {
                        var healthCheckConfiguration = new HealthCheckConfiguration
                        {
                            Name = instance.InstanceId,
                            Uri = instance.HealthCheckUrl                            
                        };
                        _db.Configurations.Add(healthCheckConfiguration);
                    }
                    else
                    {
                        configuration.Name = instance.InstanceId;
                        configuration.Uri = instance.HealthCheckUrl;                        
                        
                        _db.Configurations.Update(configuration);
                    }
                });
            });

            _db.SaveChanges();

            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}