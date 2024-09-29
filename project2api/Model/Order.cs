using System.ComponentModel.DataAnnotations;

namespace project2api.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Order name Required.")]
        public string Name { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a positive value.")]
        public decimal Totalprice { get; set; }
        
        public OrderStatus Status {  get; set; }= OrderStatus.Completed;
        

        // relationship with orderitem 1=>m

        public ICollection<OrderItem>? OrderItems { get; set; }

        // relationship between cartorder
        //public ICollection<CartOrder>? cartOrders { get; set; }

        // relationship with user
        public String? userid { get; set; }
        public AppUser? User { get; set; }


    }
    public enum OrderStatus
    {
        Pending,
        Completed,
        Canceled
    }
}
