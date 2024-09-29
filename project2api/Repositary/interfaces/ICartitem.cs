using project2api.DTO;
using project2api.DTO.OrderDTO;

namespace project2api.Repositary.interfaces
{
    public interface ICartitem
    {
        IEnumerable<CartiemDTO> GetAll();
        Task<CartiemDTO> getbyid(int id);
        Task<CartiemDTO> GetById(int id);
        string Add(CartiemDTO cartitemdto);
        Task<string> Remove(int id);
        Task<string> Update(CartiemDTO cartitemdto);
    }
}
