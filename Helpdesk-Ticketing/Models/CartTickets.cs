using Helpdesk_Ticketing.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk_Ticketing.Models
{
    public class CartTickets
    {
        public int ID { get; set; }

        [ForeignKey("Cart")]
        public int CartID { get; set; }
        public int TicketID { get; set; }

        [ForeignKey("TicketTypes")]
        public int TicketTypesID { get; set; }

        public Cart Cart { get; set; }
        public TicketTypes TicketTypes { get; set; }

        public string AccountUsersID { get; set; }

        public AccountUsers AccountUsers { get; set; }
    }
}
