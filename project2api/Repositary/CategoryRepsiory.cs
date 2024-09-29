using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project2api.Data;
using project2api.DTO;
using project2api.DTO.CateoryDTO;
using project2api.DTO.emloyeesDto;
using project2api.Model;
using project2api.Repositary.interfaces;

namespace project2api.Repositary
{
    public class CategoryRepsiory : ICategory
    {
        private readonly AppDbContext context;

        public CategoryRepsiory(AppDbContext context)
        {
            this.context = context;
        }
        public categorDTOforAdd Add(categorDTOforAdd categoryDto)
        {
            Category _category = new Category
            {

                Name=categoryDto.name,
                Mangsername=categoryDto.manger
                

            };
            context.categories.Add(_category);
            context.SaveChanges();
            return categoryDto;

        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            var categories = context.categories.Include(x=>x.products).ToList();
            var categorDto = categories.Select(x => new CategoryDTO
            {
                id = x.Id,
                name = x.Name,
                manger = x.Mangsername,
               

            }).ToList();
            return categorDto;
        }

        public Task<CategoryDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Remove(int id)
        {
            var category = await context.categories.FindAsync(id);
            if (category == null)
            {
                return "this employee dont exist";
            }
            else
            {
                context.categories.Remove(category);
                context.SaveChanges();
                return "succes remove category";
            }
        }

        public async Task<string> Update(CategoryDTO categoryDto)
        {
            var category = await context.categories.SingleOrDefaultAsync(x => x.Id == categoryDto.id);

            if (category == null)
            {
                return "this employe dont exist or name null";
            }
            else
            {
                if (!string.IsNullOrEmpty(categoryDto.name))
                {
                    category.Name = categoryDto.name;
                }
                if (!string.IsNullOrEmpty(categoryDto.manger))
                {
                    category.Mangsername = categoryDto.manger;


                }

                context.SaveChanges();
                return "sussfully update";

            }
        }
    }
}
