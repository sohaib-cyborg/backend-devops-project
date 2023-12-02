using Microsoft.AspNetCore.Identity;
using DataAccessLayer.Context;

namespace DataAccessLayer.Models
{
    public class Address
    {
        public string Name { get; set; }
        public Guid AddressId { get; set; }
        public string HouseNum { get; set; }
        public string AreaCode { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
