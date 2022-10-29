using Microservice.CQRS.Deluxe.Infrastructure.Extras.Backend.Context;
using Microservice.CQRS.Deluxe.Infrastructure.Extras.Backend.Entities;
using System;

namespace Microservice.CQRS.Deluxe.Infrastructure.Extras
{
    public class EmailService
    {
        private readonly MerloXtraContext _merloXtraContext;
        public EmailService(MerloXtraContext merloXtraContext)
        {
            _merloXtraContext = merloXtraContext;
        }
        public void Send(string address, string message)
        {
            var email = new SentEmail { Address = address, Body = message, Sent = DateTime.Now };
            _merloXtraContext.SentEmails.Add(email);
            _merloXtraContext.SaveChanges();
        }
    }
}