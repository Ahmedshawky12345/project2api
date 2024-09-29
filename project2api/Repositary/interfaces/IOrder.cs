using project2api.DTO.emloyeesDto;
using project2api.DTO.OrderDTO;

namespace project2api.Repositary.interfaces
{
    public interface IOrder
    {
        IEnumerable<orderDTO> GetAll(string userid);
        Task<orderDTO> GetById(int id);
        string Add(OrderAddDTO _orderDTO,string userid);
        Task<string> Remove(int id);
        Task<string> Update(orderDTO _orderDTO);

        
        

    }
}
