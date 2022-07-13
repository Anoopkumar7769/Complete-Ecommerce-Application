using eTickets.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public RegisterViewModel Register { get; set; }
        public List<OrderItem> OrderItem { get; set; }
        public string Order_Status { get; set; }
    }
}
