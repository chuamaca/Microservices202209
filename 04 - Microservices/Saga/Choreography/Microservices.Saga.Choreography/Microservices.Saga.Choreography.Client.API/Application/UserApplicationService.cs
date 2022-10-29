using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microservices.Saga.Choreography.Client.API.Infrastructure.Connection;
using Microservices.Saga.Choreography.Client.API.Infrastructure.Data.Context;
using Microservices.Saga.Choreography.Client.API.Infrastructure.Data.Entities;
using Microservices.Saga.Choreography.Client.API.Infrastructure.Data.Repositories;
using Microservices.Saga.Choreography.Client.API.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Microservices.Saga.Choreography.Client.API.Application {
    public class UserApplicationService : IUserApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConnectionBus _connectionBus;
        public UserApplicationService(IUserRepository userRepository, IConnectionBus connectionBus)
        {
            _userRepository = userRepository;
            _connectionBus = connectionBus;
        }
        public List<User> ListUsers()
        {
            return _userRepository.ListUsers();
        }

        public string InsertUser(UserShop userShop)
        {
            string stocData = JsonConvert.SerializeObject(userShop);
            byte[] message = Encoding.UTF8.GetBytes(stocData);
            _connectionBus.PublishMessage("Microservices.Saga.Choreography.User", message);

            return $"[x] Sent {userShop.name}";
        }
    }
}