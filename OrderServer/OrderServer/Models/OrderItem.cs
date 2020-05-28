using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace OrderServer.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int CuisineId { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Order Order { get; set; }
        public int Amount { get; set; }
    }
}
