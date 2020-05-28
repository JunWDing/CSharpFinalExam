using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace OrderServer.Models
{
    public class Cuisine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitPrice { get; set; }
        public string CuisineType { get; set; }
        public int MerchantId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Merchant Merchant { get; set; }
    }
}
