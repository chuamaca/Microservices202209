namespace Microservice.CQRS.Premium.Client.ViewModels
{
    public class ViewModelBase
    {
        public ViewModelBase()
        {
            Title = "CQRS Premium";
        }

        public string Title { get; set; } 
    }
}