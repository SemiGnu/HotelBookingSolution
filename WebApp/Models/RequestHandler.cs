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

                var bookedRoomIds = db.Booking.Where(bo => !(bo.CheckOutDate.CompareTo(rr.CheckInDate) < 0 || bo.CheckInDate.CompareTo(rr.CheckOutDate) > 0)).Select(bo => bo.RoomId);
                var bestRoom = db.Room.Where(rm => rm.NumberOfBeds >= rr.NumberOfBeds).Where(rm => !bookedRoomIds.Contains(rm.RoomId)).OrderBy(rm => rm.NumberOfBeds).FirstOrDefault();

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