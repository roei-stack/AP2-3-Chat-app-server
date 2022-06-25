using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OurMessage
    {
        [Key]
        public int key { get; set; }

        public int id { get; set; }
        public string content { get; set; }
        // true if called from the sender, false otherwise
        public bool sent { get; set; }
        public DateTime created { get; set; } = DateTime.Now;
    }
}
