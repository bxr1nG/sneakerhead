using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneakerhead.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        public string Adress { get; set; }
        public string Status { get; set; }

        public int SneakerId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public Byte[] Image { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
