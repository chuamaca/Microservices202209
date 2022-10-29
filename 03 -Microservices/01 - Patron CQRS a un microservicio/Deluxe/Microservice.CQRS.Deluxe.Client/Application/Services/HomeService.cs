using System.Collections.Generic;
using System.Linq;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data;
using Microservice.CQRS.Deluxe.QueryStack.DataAccess;
using Microservice.CQRS.Deluxe.QueryStack.DataAccess.Extensions;
using Microservice.CQRS.Deluxe.QueryStack.Model;
using Microservice.CQRS.Deluxe.Client.ViewModels;
using Microservice.CQRS.Deluxe.Infrastructure.Persistence.SqlServer.Data.Entities;

namespace Microservice.CQRS.Deluxe.Client.Application.Services
{
    public class HomeService
    {
        private readonly Database _database;
        public HomeService(Database database)
        {
            _database = database;
        }
        public IndexViewModel GetIndexViewModel()
        {            
            var courtSchedules = new List<CourtSchedule>();
            
            // Get booking for courts
            var courts = _database.Courts.ToList();
            var courtIds = (from c in courts select c.Id).Distinct().ToArray();
            var bookings = _database.Bookings.ForCourts(courtIds);

            foreach (var court in courts)
            {
                var schedule = GetScheduleForCourt(court, bookings.Where(b => b.CourtId == court.Id).ToList());
                courtSchedules.Add(schedule);
            }

            var model = new IndexViewModel();
            model.CourtSchedules = courtSchedules;
            return model;
        }

        private static CourtSchedule GetScheduleForCourt(Court court, IList<Booking> bookings)
        {
            var schedule = new CourtSchedule();
            schedule.CourtId = court.Id;
            schedule.CourtName = court.Name;

            for (var hour = court.FirstSlot; hour <= court.LastSlot; hour++)
            {
                var slot = new Slot();
                slot.StartingAt = hour;

                var matchingBooking = (from b in bookings where b.StartingAt == hour select b).FirstOrDefault();
                if (matchingBooking != null)
                {
                    slot.BookingId = matchingBooking.Id;
                    slot.Name = matchingBooking.Name;
                    slot.Length = matchingBooking.Length;
                    if (slot.Length > 1)
                        hour += (slot.Length - 1);
                }
                schedule.Slots.Add(slot);
            }
            return schedule;
        }
    }
}