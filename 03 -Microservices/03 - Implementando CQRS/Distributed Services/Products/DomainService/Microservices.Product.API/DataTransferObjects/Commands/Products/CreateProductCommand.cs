namespace Microservices.Products.API.DataTransferObjects.Commands.Products
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
