namespace Web.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
        
        public Person Person { get; set; }
    }
}