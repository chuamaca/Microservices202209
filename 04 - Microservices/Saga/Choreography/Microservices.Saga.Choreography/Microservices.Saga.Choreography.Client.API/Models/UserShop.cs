
namespace Microservices.Saga.Choreography.Client.API.Models
{
    public class UserShop
    {        
        public string name { get; set; }
        public string surname { get; set; }
        public int id { get; set; }
        public int no { get; set; }
        public string productName { get; set; }
        public decimal? productPrice { get; set; }
    }
}