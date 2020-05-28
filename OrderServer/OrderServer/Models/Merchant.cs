using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderServer.Models
{
    public class Merchant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }
        public string BusinessHours { get; set; }
        public int Balance { get; set; }
        public List<Cuisine> Cuisines { get; set; }
    }
}
