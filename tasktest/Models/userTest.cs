using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tasktest.Models
{
    public class userTest
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Category { get; set; }
        public string Keyname { get; set; }
        public string Value { get; set; }
        public string Status { get; set; }
    }
}
