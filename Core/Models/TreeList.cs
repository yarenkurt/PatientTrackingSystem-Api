using System.Collections.Generic;

namespace Core.Models
{
    public class TreeList
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<TreeList> Sub { get; set; }
    }
}