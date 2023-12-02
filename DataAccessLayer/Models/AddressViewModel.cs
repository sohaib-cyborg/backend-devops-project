namespace DataAccessLayer.Models
{
    public class AddressViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HouseNum { get; set; }
        public string AreaCode { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
    }
}
