using System.Collections.Generic;

namespace eTickets.Models.Account
{
    public class Dropdowns
    {
        public Dropdowns()
        {
            RoleName = new List<RoleName>();
        }
        public List<RoleName> RoleName { get; set; }
    }
}
