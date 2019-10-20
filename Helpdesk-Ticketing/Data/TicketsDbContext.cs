using Helpdesk_Ticketing.Models;
using static Helpdesk_Ticketing.Models.TicketTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpdesk_Ticketing.Models.ViewModels;

namespace Helpdesk_Ticketing.Data
{
    public class TicketsDbContext : DbContext
    {
        public TicketsDbContext(DbContextOptions<TicketsDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketTypes>().HasData(
                new TicketTypes
                {
                    ID = 1,
                    NameTicket = "Problem 1",
                    //Importance = Priority.Low,
                    Comments = "details:  "
                },
                new TicketTypes
                {
                    ID = 2,
                    NameTicket = "Problem 2",
                    Comments = "details:  "
                },
                new TicketTypes
                {
                    ID = 3,
                    NameTicket = "Problem 3",
                    Comments = "details:  "
                },
                new TicketTypes
                {
                    ID = 4,
                    NameTicket = "Problem 4",
                    Comments = "details:  "
                },
                new TicketTypes
                {
                    ID = 5,
                    NameTicket = "Problem 5",
                    Comments = "details:  "
                }
                );
        }

        public DbSet<TicketTypes> TicketTypes { get; set; }
        public DbSet<CartTickets> CartTickets { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
