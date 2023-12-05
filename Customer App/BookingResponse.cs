using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_App
{
    public class BookingResponse
    {
        public string uploadResponseCode { get; set; }
        public string userId { get; set; }
        public int number { get; set; }
        public string bookings { get; set; }
        public string message { get; set; }

    }
}
