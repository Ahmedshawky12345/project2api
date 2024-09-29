namespace project2api.Model
{
    public class product
    {
        public int id { get; set; }
        public string name { get; set; }
      
        public double price { get; set; }
        public int quantity { get; set; }
        public string description { get; set; }

        //public string ImageUrl { get; set; }


        //relation with Dempartment

        public int? cat_id { get; set; }
        public Category? category { get; set; }

        // relationship with cartitem

        public ICollection<CartItem>? cartItems { get; set; }

        // relationship with orderitem
        public ICollection<OrderItem>? orderItems { get; set; }


    }
}
