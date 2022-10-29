using System;

namespace Microservices.Infrastructure.Crosscutting
{
    public abstract class ReadObject
    {
        public Guid Id { get; set; }
    }
}