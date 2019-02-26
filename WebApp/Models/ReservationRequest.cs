using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ReservationRequest
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfBeds { get; set; }
        public string Username { get; set; }

        public bool isValid() {
            return CheckInDate != null && CheckOutDate != null && NumberOfBeds > 0 && Username != null;
        }
    }
}