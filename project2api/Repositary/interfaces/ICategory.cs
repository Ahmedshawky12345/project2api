using project2api.DTO.CateoryDTO;
using project2api.DTO.emloyeesDto;

namespace project2api.Repositary.interfaces
{
    public interface ICategory
    {
        IEnumerable<CategoryDTO> GetAll();
        Task<CategoryDTO> GetById(int id);
        categorDTOforAdd Add(categorDTOforAdd categoryDto);
        Task<string> Remove(int id);
        Task<string> Update(CategoryDTO categoryDto);

    }
}
