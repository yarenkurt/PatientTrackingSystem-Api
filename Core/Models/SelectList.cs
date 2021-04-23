namespace Core.Models
{
    public class SelectList
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public SelectList()
        {
            
        }

        public SelectList(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}