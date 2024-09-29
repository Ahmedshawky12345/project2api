namespace project2api.Model
{
    public class Cart
    {
        public int id { get;set; }
        public string name { get;set; }
        
        // relationship with user
        public string? userid { get;set; }   
        public AppUser? Appuser {  get;set; }

        // relationship with cartitem
        public ICollection<CartItem>? cartItems { get;set; }
      

        
    }
}
