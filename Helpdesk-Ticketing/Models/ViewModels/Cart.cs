using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk_Ticketing.Models.ViewModels
{
    public class Cart
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public List<CartTickets> CartTickets { get; set; }
    }
}
