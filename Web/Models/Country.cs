using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Web.Models
{
    public class Country 
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CountryCode { get; set; }
    }
}