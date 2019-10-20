using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Helpdesk_Ticketing.Models
{
    public class TicketTypes
    {
        public int ID { get; set;}

        public string NameTicket { get; set; }
        /*  I would of liked to show an enum drop down option but VS wont recognize yet.
        public enum Priority
        {
            Low,
            Medium,
            High
        }
        */
        public string Comments { get; set; }//Not sure on this one. 
        //I will have to think on how to have user add comments or description to tickets.
    }
}
