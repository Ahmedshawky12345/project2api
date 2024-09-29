using System.ComponentModel.DataAnnotations;

namespace project2api.Model
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
       
        public string Name { get; set; }
        [Range(0,int.MaxValue,ErrorMessage ="enter positive value please")]
        public int Quntity { get; set; }
        public decimal price { get; set; }

        // ---------relationship between order m=>1
        public int orderId { get; set; }
        public Order? order { get; set; }

        // relationship with product

        public int? productId { get; set; }  
        public product? product { get; set; }
    }
}
