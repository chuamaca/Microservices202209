using Microservice.Events.MessageSubscriber.Interfaces;
using Microservice.Events.MessageSubscriber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Events.EmailService.Application
{
    public class EmailProcessor : IMessageProcessor
    {
        public void Process(Message message)
        {
            var email = (Email)message.Data.DeSerializeFromJSON(typeof(Email));

            Logger.LogInfo("---------------------------------------------------------------");
            Logger.LogInfo($"Email Queue Message received for processing...");
            Logger.LogInfo($"Sending email to {email.ToAddress}");
            Logger.LogInfo($"with subject {email.Subject}");
            Logger.LogInfo($"with message {email.Message}");
            Logger.LogInfo("---------------------------------------------------------------");
        }
    }
}
