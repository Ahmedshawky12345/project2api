using Microsoft.EntityFrameworkCore;
using project2api.Data;
using project2api.DTO.OrderDTO;
using project2api.DTO.OrderitemDTO;
using project2api.Model;
using project2api.Repositary.interfaces;

namespace project2api.Repositary
{
    public class OrderitemRepsitory : IOrderItem
    {
        private readonly AppDbContext context;

        public OrderitemRepsitory(AppDbContext context)
        {
            this.context = context;
        }
        public string Add(Orderitemadd _orderitemDTO)
        {
            
            OrderItem _orderitem = new OrderItem
            {

               Name=_orderitemDTO.name,
               price=_orderitemDTO.price,
               Quntity=_orderitemDTO.quntity,
               orderId = _orderitemDTO.orderid,
              
               


            };
            context.orderItems.Add(_orderitem);
            context.SaveChanges();

            return "success to add orderitem";
        }

        public IEnumerable<orderitemDTO> GetAll()
        {
            var orderitem = context.orderItems.ToList();
            var orderdto = orderitem.Select(x => new orderitemDTO
            {
                
                id=x.Id,
                name=x.Name,
                price=x.price,
                quntity=x.Quntity,
                orderid=x.orderId

            }).ToList();
            return orderdto;
        }

        public async Task<orderitemDTO> GetById(int id)
        {
            var orderitem = await context.orderItems.SingleAsync(x => x.Id == id);
            if (orderitem == null)
            {
                return null;
            }
            else
            {
                orderitemDTO orderdto = new orderitemDTO
                {
                    id = orderitem.Id,
                    name = orderitem.Name,
                    price = orderitem.price,
                    quntity = orderitem.Quntity,
                    orderid=orderitem.orderId
                    

                };
                return orderdto;
            }
        }

        public async Task<string> Remove(int id)
        {
            var orderitem = await context.orderItems.FindAsync(id);
            if (orderitem == null)
            {
                return "this employee dont exist";
            }
            else
            {
                context.orderItems.Remove(orderitem);
                context.SaveChanges();
                return "succes remove category";
            }
        }

        public async Task<string> Update(orderitemDTO _orderitemDTO)
        {
            var orderitem = await context.orderItems.SingleOrDefaultAsync(x => x.Id == _orderitemDTO.id);

            if (orderitem == null)
            {
                return "this employe dont exist or name null";
            }
            else
            {
                if (!string.IsNullOrEmpty(_orderitemDTO.name))
                {
                    orderitem.Name = _orderitemDTO.name;


                }
                if (_orderitemDTO.price.HasValue)
                {
                    orderitem.price = _orderitemDTO.price.Value;


                }
                if (_orderitemDTO.quntity.HasValue)
                {
                    orderitem.Quntity = _orderitemDTO.quntity.Value;

                }
          
                orderitem.orderId = _orderitemDTO.orderid;
                
                    

                
                context.SaveChanges();
                return "sussfully update";

            }
        }
    }
}
