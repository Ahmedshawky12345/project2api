using Microsoft.AspNetCore.Identity;

namespace project2api.Model
{
    public class AppUser:IdentityUser
    {
      
        public Cart? cart { get; set; }
        public ICollection<Order> Orders {  get; set; }
    }
}
