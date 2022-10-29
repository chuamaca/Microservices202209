using Microservice.CQRS.Regular.Client.Models;
using Microservice.CQRS.Regular.CommandStack.Context;
using Microservice.CQRS.Regular.CommandStack.Entities;
using Microservice.CQRS.Regular.ReadStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.CQRS.Regular.Client.Application
{
    public class AdminApplicationService:IAdminApplicationService
    {
        private readonly Database _database;
        private readonly CommandDbContext _commandDbContext;
        public AdminApplicationService(Database database,CommandDbContext commandDbContext)
        {
            _database = database;
            _commandDbContext = commandDbContext;
        }
        public RegisterViewModel GetAdminViewModel()
        {
            var model = new RegisterViewModel();
            var list = (from m in _database.Matches select m).ToList();
            model.Matches = list;


            return model;
        }

        public void Register(RegisterInputModel input)
        {
            // Push a command through the stack
           
            var match = new Match { Id = input.Id, Team1 = input.Team1, Team2 = input.Team2 };
            _commandDbContext.Matches.Add(match);
            _commandDbContext.SaveChanges();

        }
    }
}
