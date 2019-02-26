using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RequestHandler
    {
        public static Booking HandleRequest(ReservationRequest rr)
        {
            Booking returnBooking = null;


            if (rr.isValid())
            {
                dat154_19_2Entities db = new dat154_19_2Entities();

                var bestRoom = db.Room.Where(r => r.NumberOfBeds >= rr.NumberOfBeds)
                    .Join(db.Booking,
                    r => r.RoomId,
                    b => b.RoomId,
                    (r, b) => new { R = r, B = b })
                    .Where(rb => rb.B.CheckOutDate <= rr.CheckInDate || rb.B.CheckInDate >= rr.CheckOutDate)
                    .OrderBy(rb => rb.R.NumberOfBeds)
                    .Select(rb => rb.R)
                    .FirstOrDefault();

                if (bestRoom != null)
                {
                    returnBooking = new Booking
                    {
                        CustomerUsername = rr.Username,
                        CheckInDate = rr.CheckInDate,
                        CheckOutDate = rr.CheckOutDate,
                        RoomId = bestRoom.RoomId
                    };
                }
                
            }
            return returnBooking;
        }
    }
}