using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project2api.Data;
using project2api.DTO.CateoryDTO;
using project2api.DTO.emloyeesDto;
using project2api.DTO.ProductDto;
using project2api.Migrations;
using project2api.Model;
using project2api.Repositary.interfaces;

namespace project2api.Repositary
{
    public class ProductRepsitory : IProduct
    {
        private readonly AppDbContext context;

        public ProductRepsitory(AppDbContext context)
        {
            this.context = context;
        }
        public productAddDTO Add(productAddDTO _productDTO)
        {

            product products = new product
            {
                
                name = _productDTO.name,
                price=_productDTO.price,
                description=_productDTO.description,
                quantity=_productDTO.quantity,
                cat_id=_productDTO.catid

            };
            context.Products.Add(products);
            context.SaveChanges();
            return _productDTO;
           

        }

        public  IEnumerable<productDTO> GetAll()
        {
            var products =  context.Products.ToList();
            if (products == null)
            {
                return null;
            }
           else {
                var productDto = products.Select(x => new productDTO
                {
                    id=x.id,
                    name = x.name,
                    price=x.price,
                    description=x.description,
                    quantity=x.quantity,
                    catid=x.cat_id
                    
                    
                    
                }).ToList();
                return productDto;
            }
          
        }

        public async Task<productDTO> GetById(int id)
        {
            var product = await context.Products.SingleOrDefaultAsync(x => x.id == id);
            if (product == null)
            {
                return null;
            }
            else
            {
                productDTO productdto = new productDTO
                {
                    id = product.id,
                    name = product.name,
                    price=product.price,
                    description=product.description,
                    quantity=product.quantity,
                    catid=product.cat_id
                    
                    
                };
                return productdto;
            }
        }

        public async Task<prodectandcategoryDTO> GetProductAndCategory(int id)
        {
           var product= await context.Products.Include(x=>x.category).FirstOrDefaultAsync(x=>x.id==id);
            if(product == null)
            {
                return null;
            }
            else
            {
                var productdto = new prodectandcategoryDTO
                {
                    id = product.id,
                    name = product.name,
                    price = product.price,
                    description = product.description,
                    quntity = product.quantity,
                    Categories = product.category.Name

                };

                return productdto;


            }
        }

        public async Task<string> Remove(int id)
        {
            var employee =await context.Products.FindAsync(id);
            if (employee == null)
            {
                return "product dont exist";
            }
            else
            {
               context.Products.Remove(employee);
                context.SaveChanges();
                return "succes remove this Product";
            }
        }

        public async Task<string> Update(productDTO _productDTO)
        {
            var emp =await context.Products.SingleOrDefaultAsync(x => x.id == _productDTO.id);
           
            if (emp == null)
            {
                return "this Product dont exist ";
            }
            else
            {
                if (!string.IsNullOrEmpty(_productDTO.name))
                {
                    emp.name = _productDTO.name;
                }
                if (!string.IsNullOrEmpty(_productDTO.description))
                {
                    emp.description = _productDTO.description;
                }

                if (_productDTO.price.HasValue)
                {
                    emp.price = _productDTO.price.Value;

                }

                if (_productDTO.quantity.HasValue)
                {
                    emp.quantity = _productDTO.quantity.Value;

                }

                if (_productDTO.catid.HasValue)
                {
                    emp.cat_id = _productDTO.catid.Value;

                }
                if (_productDTO.catid.HasValue)
                {
                    emp.cat_id = _productDTO.catid.Value;
                }

                context.Products.Update(emp);
                context.SaveChanges();
                return "sussfully update";

            }
            
        }
    }
}
