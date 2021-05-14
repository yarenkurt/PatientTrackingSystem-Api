namespace Entities.Dtos
{
    public class GetHospitalDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public string DistrictName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }

    }
}