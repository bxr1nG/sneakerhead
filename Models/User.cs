using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneakerhead.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FIO { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }
        public List<Order> Order { get; set; } = new List<Order>();
        public List<Sneaker> Sneaker { get; set; } = new List<Sneaker>();
    }
}
