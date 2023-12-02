namespace DataAccessLayer.Models
{
    public class UpdateAddressViewModel
    {
        public string Name { get; set; }
        public Guid AddressId { get; set; }
        public string HouseNum { get; set; }
        public string AreaCode { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
    }
}
