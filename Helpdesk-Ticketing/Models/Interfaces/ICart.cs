using Helpdesk_Ticketing.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk_Ticketing.Models.Interfaces
{
    public interface ICart
    {
        Task<Cart> UpdateCartTickets(int id, string username);

        Task<CartTickets> DeleteTicket(int id);

        Task<bool> DeleteCartTickets(string username);

        Task<Cart> GetCartForUser(string username);
    }
}
