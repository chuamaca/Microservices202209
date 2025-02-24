﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Events.EmailService.Application
{
    public class Email
    {
        public Email(string emailaddress, string emailsubject, string emailmessage)
        {
            this.ToAddress = emailaddress;
            this.Subject = emailsubject;
            this.Message = emailmessage;
        }

        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
