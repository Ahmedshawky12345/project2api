using Microsoft.EntityFrameworkCore;
using project2api.Data;
using project2api.DTO;
using project2api.DTO.CateoryDTO;
using project2api.DTO.emloyeesDto;
using project2api.Model;
using project2api.Repositary.interfaces;

namespace project2api.Repositary
{
    public class CartitemRepsitory : ICartitem
    {
        private readonly AppDbContext context;

        public CartitemRepsitory(AppDbContext context)
        {
            this.context = context;
        }
        public string Add(CartiemDTO cartitemdto)
        {
            var _cartitem = new CartItem
            {

                Id= cartitemdto.id,
               Quantity=cartitemdto.quntity.Value


            };
            context.cartItems.Add(_cartitem);
            context.SaveChanges();
            return " the cartitem add sucsses";
        }

        public IEnumerable<CartiemDTO> GetAll()
        {
            var cartitem = context.cartItems.ToList();
            if (cartitem == null)
            {
                return null;
            }
            else
            {
                var cartitemdto = cartitem.Select(x => new CartiemDTO
                {
                   id=x.Id,
                   quntity=x.Quantity,



                }).ToList();
                return cartitemdto;
            }
        }

        public async Task<CartiemDTO> GetById(int id)
        {
            var cartitem = await context.cartItems.SingleOrDefaultAsync(x => x.Id == id);
            if (cartitem == null)
            {
                return null;
            }
            else
            {
                CartiemDTO cartitemdto = new CartiemDTO
                {
                    id= cartitem.Id,
                    quntity=cartitem.Quantity


                };
                return cartitemdto;
            }
        }

        public async Task<CartiemDTO> getbyid(int id)
        {
            var cartitem = await context.cartItems.SingleOrDefaultAsync(x => x.Id == id);
            if (cartitem == null)
            {
                return null;
            }
            else
            {
                var cartitemdto = new CartiemDTO
                {
                    id=cartitem.Id,
                    quntity=cartitem.Quantity


                };
                return cartitemdto;
            }
        }

        public async Task<string> Remove(int id)
        {
            var cartitem = await context.cartItems.SingleOrDefaultAsync(x=>x.Id==id);
            if (cartitem == null)
            {
                return "this employee dont exist";
            }
            else
            {
                context.cartItems.Remove(cartitem);
                context.SaveChanges();
                return "succes remove category";
            }
        }

        public async Task<string> Update(CartiemDTO cartitemdto)
        {
            var cartitem = await context.cartItems.SingleOrDefaultAsync(x => x.Id == cartitemdto.id);

            if (cartitem == null)
            {
                return "this employe dont exist or name null";
            }
            else
            {
                if (cartitemdto.quntity.HasValue)
                {
                    cartitem.Quantity = cartitemdto.quntity.Value;
                }
               

                context.SaveChanges();
                return "sussfully update";

            }
        }
    }
}
