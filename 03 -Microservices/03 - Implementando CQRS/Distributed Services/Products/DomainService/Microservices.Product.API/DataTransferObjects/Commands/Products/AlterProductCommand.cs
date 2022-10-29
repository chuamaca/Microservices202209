namespace Microservices.Products.API.DataTransferObjects.Commands.Products
{
    public class AlterProductCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Version { get; set; }
    }
}
