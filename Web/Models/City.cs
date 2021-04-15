using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Web.Models
{
    public class City 
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }

    }
}