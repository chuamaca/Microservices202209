using Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Data.Context;
using Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Saga.Choreography.UserDetail.Worker.Application
{
    using Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Connection;
    using Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Connection.RabbitMQ;
    using Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Data.Entities;
    using Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Enums;
    using Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Extensions;
    using Microservices.Saga.Choreography.UserDetail.Worker.Models;
    using System.Text.Json;

    public class UserDetailApplicationService : IUserDetailApplicationService
    {
        public void ProcessUserDetail(UserDetailQueue userDetailQueue)
        {
            Console.WriteLine("\n");
            Console.WriteLine($"Received UserDetail; User : {userDetailQueue.User.Name} {userDetailQueue.User.Surname}; Product : {userDetailQueue.Product.Name}, Price : {userDetailQueue.Product.Price}");

            UserDetail newUserDetail = AddUserDetail(userDetailQueue);
            CommitTransaction(userDetailQueue.TransactionList);
        }

        private void CommitTransaction(List<TransactionHistory> transactionList)
        {
            SagaChoreographyContext context = new SagaChoreographyContext();

            foreach (TransactionHistory dataTransaction in transactionList)
            {
                string TableName = dataTransaction.TableName;
                int ID = dataTransaction.ID;
                
                if (dataTransaction.Type == TransactionType.SqlDB && dataTransaction.State == TransactionState.Pending)
                {
                    string updateSQL = $"UPDATE {TableName}  SET IsActive = 1 WHERE Id = {ID}";

                    context.ExecuteQuery(updateSQL);
                    dataTransaction.State = TransactionState.Completed;

                    Console.WriteLine(updateSQL);
                }

                //Run Different Bussines Logic By Step Name
                if (dataTransaction.Step == TransactionStep.Product && dataTransaction.State == TransactionState.Completed)
                {

                }
            }
        }

        private UserDetail AddUserDetail(UserDetailQueue userDetailQueue)
        {
            IUserDetailRepository userDetailRepository = new UserDetailRepository(new SagaChoreographyContext());

            UserDetail newUserDetail = new UserDetail();
            newUserDetail.UserId = userDetailQueue.UserId;
            newUserDetail.ProductId = userDetailQueue.ProductId;
            newUserDetail.CreatedDate = DateTime.Now;
            newUserDetail.IsActive = true;
            userDetailRepository.Add(newUserDetail);
                        
            Console.WriteLine($"Save UserDetail; User : {userDetailQueue.User.Name} {userDetailQueue.User.Surname}; Product : {userDetailQueue.Product.Name}, Price : {userDetailQueue.Product.Price}");

            return newUserDetail;
        }
    }
}
