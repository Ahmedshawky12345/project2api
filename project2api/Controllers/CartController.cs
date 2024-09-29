using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project2api.Data;
using project2api.DTO;
using project2api.DTO.emloyeesDto;
using project2api.DTO.User;
using project2api.Model;
using System.Security.Claims;

namespace project2api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> usermanger;

        public CartController(AppDbContext context , UserManager<AppUser> usermanger)
        {
            this.context = context;
            this.usermanger = usermanger;
        }
        GenralRespnse genralrespnse = new GenralRespnse();
        // add new task for spacifc imployee
        [HttpGet]
        public async Task<ActionResult> getcartforloginuser()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userid))
            {
                return Unauthorized("User not authenticated");
            }

            var user = await usermanger.Users.Include(x => x.cart).ThenInclude(x => x.cartItems).Where(x => x.Id == userid)
                .Select(x => new UserDTO
                {
                    Id = x.Id,
                    //Email = x.Email,
                    //Name = x.UserName,
                    Cart = new cartDTO
                    {
                        id = x.cart.id,
                        //name = x.cart.name,
                        cartiemdto = x.cart.cartItems.Select(y => new CartiemDTO
                        {
                            //id = y.Id,
                           
                            product=y.product.name,
                             quntity = y.Quantity



                        }).ToList()
                    }
                }).FirstOrDefaultAsync();

            if (user  == null)
            {
                return NotFound("User or Cart not found.");
            }
            return Ok(user);
        }
        [HttpPost("addtocart/{productId}")]
        public async Task<IActionResult> AddToCart(int productId)
        {
            // Step 1: Get the logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            // Step 2: Find the product
            var product = await context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Step 3: Check if the user has an existing cart
            var cart = await context.Carts
                .Include(c => c.cartItems)
                .FirstOrDefaultAsync(c => c.userid == userId);

            if (cart == null)
            {
                // Step 4: Create a new cart for the user if it doesn't exist
                cart = new Cart
                {
                    userid = userId,
                    name = User.Identity.Name // Can be username or any identifier
                };
                context.Carts.Add(cart);
            }

            // Step 5: Check if the product is already in the cart
            var cartItem = cart.cartItems.FirstOrDefault(ci => ci.productid == productId);
            if (cartItem != null)
            {
                // Step 6: If the product is already in the cart, increase the quantity
                cartItem.Quantity++;
            }
            else
            {
                // Step 7: If the product is not in the cart, add it as a new item
                cartItem = new CartItem
                {
                    cartId = cart.id,
                    productid = product.id,
                    Quantity = 1
                    
                };
                cart.cartItems.Add(cartItem);
            }

            // Step 8: Save changes to the database
            await context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Product added to cart successfully.",
                CartItem = new CartiemDTO
                {
                    id=cartItem.Id,
                    quntity=cartItem.Quantity
                    ,productid=cartItem.productid.Value
                    
                }
            });
        }

        // Delte spacifcic product for cart

        [HttpDelete]
        public async Task<ActionResult> DeletefromCart(int productid)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userid))
            {
                return Unauthorized();
            }
            // if this user has cart or not

            var cart = await context.Carts.Include(x => x.cartItems).FirstOrDefaultAsync(x => x.userid == userid);
            if (cart == null)
            {
                return NotFound("this user dont has the cart");
            }
            var user = await usermanger.Users.Include(x => x.cart).ThenInclude(x => x.cartItems)
                .FirstOrDefaultAsync(x=>x.Id==userid);
            //------ find product in cart or not

           

            // cheack if product in cart or not 
            var cartitem =  user.cart.cartItems.FirstOrDefault(x => x.productid == productid);
            if (cartitem == null)
            {
                return NotFound("this product dont exist in cart to remove it");

            }
            user.cart.cartItems.Remove(cartitem);
           await context.SaveChangesAsync();

            return Ok("the product removed sussfully");





            

        }
        [HttpDelete("clear cart")]
        public async Task<ActionResult> clearcart()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userid))
            {
                return Unauthorized();
            }
            // if this user has cart or not

            var cart = await context.Carts.Include(x => x.cartItems).FirstOrDefaultAsync(x => x.userid == userid);
            if (cart == null)
            {
                return NotFound("this user dont has the cart");
            }
            var user = await usermanger.Users.Include(x => x.cart).ThenInclude(x => x.cartItems)
                .FirstOrDefaultAsync(x => x.Id == userid);
            //------ find product in cart or not



           
            user.cart.cartItems.Clear();
            await context.SaveChangesAsync();

            return Ok("the product removed sussfully");







        }

        // clculate totalprice
        [HttpGet("TotalPrice")]

        public async Task<ActionResult> Totalprice()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userid))
            {
                return Unauthorized();
            }
            var user = await usermanger.Users.Include(x => x.cart).ThenInclude(x => x.cartItems).ThenInclude(x=>x.product)
                .FirstOrDefaultAsync(x => x.Id == userid);
         var   Totalprice = user.cart.cartItems.Sum(x => x.product.price * x.Quantity);

            return Ok("Total price is"+ Totalprice);

        }


    }
}
