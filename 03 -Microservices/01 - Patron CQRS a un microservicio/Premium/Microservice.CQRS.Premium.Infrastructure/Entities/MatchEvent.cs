﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace Microservice.CQRS.Premium.Infrastructure.Entities
{
    public partial class MatchEvent
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public int? TeamId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string MatchId { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int? PlayerId { get; set; }
    }
}