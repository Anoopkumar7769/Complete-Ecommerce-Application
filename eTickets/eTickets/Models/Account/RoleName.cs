using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models.Account
{
    public class RoleName
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
