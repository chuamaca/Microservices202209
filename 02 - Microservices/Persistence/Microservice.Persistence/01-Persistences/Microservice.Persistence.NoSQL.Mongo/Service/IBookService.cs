using Microservice.Persistence.NoSQL.Mongo.Data.Model;
using System.Collections.Generic;

namespace Microservice.Persistence.NoSQL.Mongo.Service
{
    public interface IBookService
    {
        Book Create(Book book);
        List<Book> Get();
        Book Get(string id);
        void Remove(Book bookIn);
        void Remove(string id);
        void Update(string id, Book bookIn);
    }
}