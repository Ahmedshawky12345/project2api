using project2api.DTO.OrderDTO;
using project2api.DTO.OrderitemDTO;

namespace project2api.Repositary.interfaces
{
    public interface IOrderItem
    {
        IEnumerable<orderitemDTO> GetAll();
        Task<orderitemDTO> GetById(int id);
        string Add(Orderitemadd _orderitemDTO);
        Task<string> Remove(int id);
        Task<string> Update(orderitemDTO _orderitemDTO);
    }
}
