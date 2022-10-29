using Microservice.CQRS.Regular.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.CQRS.Regular.Client.Application
{
    public interface IAdminApplicationService
    {
        RegisterViewModel GetAdminViewModel();
        void Register(RegisterInputModel input);
    }
}
