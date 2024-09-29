using project2api.DTO.OrderitemDTO;
using project2api.Model;

namespace project2api.DTO.OrderDTO
{
    public class orderDTO
    {
           public int id { get; set; }
            public string? ordernam{  get; set; }
            public decimal? totalprice {  get; set; }
            public string? status { get; set; }
        public ICollection<orderitemDTO> orderitemsDto { get; set; }
    }
}
