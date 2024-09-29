namespace project2api.DTO.OrderitemDTO
{
    public class orderitemDTO
    {
        public int id { get; set; }
        public string? name { get; set; }
        public decimal? price { get; set; }
        public int? quntity { get; set; }
        public int orderid {  get; set; }
        //public ICollection<orderitemDTO> orderitems { get; set; }
    }
}
