using Microservice.CQRS.Deluxe.CommandStack.Commands;

namespace Microservice.CQRS.Deluxe.Client.Application.Services
{
    public class BookingService
    {
        private readonly BookingApplication _bookingApplication;
        public BookingService(BookingApplication bookingApplication)
        {
            _bookingApplication = bookingApplication;
        }
        public void AddBooking(int courtId, int hour, int length, string name)
        {
            // Place the command to the bus
            var command = new RequestBookingCommand(
                courtId,
                hour,
                length,
                name
                );
            _bookingApplication.Bus.Send(command);
        }
    }
}