using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderServer.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }
        public int Balance { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public List<Order> Orders { get; set; }

    }
}
