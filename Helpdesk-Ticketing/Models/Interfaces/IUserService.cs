using Helpdesk_Ticketing.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk_Ticketing.Models.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> All();
        void Add(AccountUsers accountUsers);
        bool Exists(string id);
    }
}
