using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using project2api.Data;
using project2api.DTO.CateoryDTO;
using project2api.DTO.emloyeesDto;
using project2api.DTO.OrderDTO;
using project2api.DTO.OrderitemDTO;
using project2api.Model;
using project2api.Repositary.interfaces;

namespace project2api.Repositary
{
    public class OrderRepsitory : IOrder
    {
        private readonly UserManager<AppUser> usermanger;
        private readonly AppDbContext context;

        public OrderRepsitory(AppDbContext context,UserManager<AppUser>usermanger)
        {
            this.context = context;
            this.usermanger = usermanger;
        }

        

        public string Add(OrderAddDTO _orderDTO,string userid)
        {
           
            if (!Enum.TryParse<OrderStatus>(_orderDTO.status, true, out var orderStatus))
            {
                return "please enter the Correct status choisse from this list( Pending,  Completed,  Canceled)";
            }

            Order _order = new Order
            {
                Name = _orderDTO.ordernam,

                Status = orderStatus,

                userid = userid,
                OrderItems = _orderDTO.orderitemsDto.Select(x => new OrderItem
                {
                    Name = x.name,
                    Quntity = x.quntity.Value,
                    price = x.price.Value
                    
                }).ToList()

            };

            context.Orders.Add(_order);
            context.SaveChanges();
            return "success to add order";
        }
            

        

        public IEnumerable<orderDTO> GetAll(string userid)
        {
            
            var orders = context.Orders.Where(x=>x.userid== userid).ToList();
            var orderdto = orders.Select(x => new orderDTO
            {
                id = x.Id,
                ordernam = x.Name,
                totalprice = x.Totalprice,
                status = x.Status.ToString()


            }).ToList();
            return orderdto;
        }

        public async Task<orderDTO> GetById(int id)
        {
            var order = await context.Orders.SingleAsync(x => x.Id == id);
            if (order == null)
            {
                return null;
            }
            else
            {
                orderDTO orderdto = new orderDTO
                {
                    id= order.Id,
                    ordernam=order.Name,
                    totalprice=order.Totalprice,
                    status=order.Status.ToString()

                };
                return orderdto;
            }
        }

       

        public async Task<string> Remove(int id)
        {

            var order = await context.Orders.FindAsync(id);
            if (order == null)
            {
                return "this employee dont exist";
            }
            else
            {
                context.Orders.Remove(order);
                context.SaveChanges();
                return "succes remove category";
            }
        }

        public async Task<string> Update(orderDTO _orderDTO)
        {
            var order = await context.Orders.SingleOrDefaultAsync(x => x.Id == _orderDTO.id);

            if (order == null)
            {
                return "this employe dont exist or name null";
            }
            else
            {

                if (!string.IsNullOrEmpty(_orderDTO.ordernam))
                {
                    order.Name = _orderDTO.ordernam;
                }

                if (_orderDTO.totalprice.HasValue)
                {
                    order.Totalprice = _orderDTO.totalprice.Value; 
                }
                if (!string.IsNullOrEmpty(_orderDTO.status))
                {
                    if (!Enum.TryParse<OrderStatus>(_orderDTO.status, true, out var orderStatus))
                    {
                        return "please enter the Correct status choisse from this list( Pending,  Completed,  Canceled)";
                    }
                    else
                    {
                        order.Status = orderStatus;
                    }
                }

                context.SaveChanges();
                return "sussfully update";

            }
        }
    }
}
