using project2api.DTO.emloyeesDto;
using project2api.DTO.ProductDto;

namespace project2api.Repositary.interfaces
{
    public interface IProduct
    {
       IEnumerable<productDTO> GetAll();
        Task<productDTO> GetById(int id);
       productAddDTO Add(productAddDTO _productDTO);
        Task<string> Remove(int id);
        Task<string> Update(productDTO _productDTO);
        Task<prodectandcategoryDTO> GetProductAndCategory(int id);
       
    }
}
