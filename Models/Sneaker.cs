using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneakerhead.Models
{
    public class Sneaker
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public Byte[] Image { get; set; }
        public List<User> User { get; set; } = new List<User>();
    }
}
