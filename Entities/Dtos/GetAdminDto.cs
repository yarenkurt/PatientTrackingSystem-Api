namespace Entities.Dtos
{
    public class GetAdminDto
    {
        public int PersonId { get; set; }
        public bool IsBlocked { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gsm { get; set; }
    }
}