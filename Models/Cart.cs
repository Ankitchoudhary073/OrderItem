using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderItem.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int userId { get; set; }
        public int menuItemId { get; set; }
        public string menuItemName { get; set; }
    }
}
