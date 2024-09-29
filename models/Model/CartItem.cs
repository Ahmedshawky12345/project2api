namespace project2api.Model
{
    public class CartItem
    {

        public int Id { get; set; }
        public int Quantity { get; set; }

        // relationship with cart one => many
        public int? cartId { get; set; }
        public Cart? cart { get; set; }

        
        // relationship with product
        public int? productid { get; set; }
        public product? product { get; set; }
    }
}
