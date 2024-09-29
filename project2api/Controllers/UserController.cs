using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project2api.Data;
using project2api.DTO.OrderDTO;
using project2api.DTO.User;
using project2api.Model;
using System.Security.Claims;

namespace project2api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> usermanger;
        private readonly AppDbContext context;

        public UserController(UserManager<AppUser>usermanger,AppDbContext context)
        {
            this.usermanger = usermanger;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var user = usermanger.Users.Select(x => new UserDTO
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.UserName
            }).ToList();
            
            if (user == null)
            {
                return NotFound("any user exist");
            }
            return Ok(user);
          

        }

        // --------------- get userbyId-------------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await usermanger.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var userDTO = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.UserName
            };
            return Ok(userDTO);
         
            
        }

        //------------------ update user ---------------------------

        [HttpPut("update")]
        public async Task<IActionResult> updateuser( UserDTO userdto)
        {
            var user = await usermanger.FindByIdAsync(userdto.Id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(userdto.Name))
                {
                    user.UserName = userdto.Name;

                }
                if (!string.IsNullOrEmpty(userdto.Email))
                {
                    user.Email = userdto.Email;

                }
               
             var res=  await  usermanger.UpdateAsync(user);
                if (res.Succeeded)
                {
                    return Ok("User updated successfully");
                }
                foreach(var i in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, i.Description);
                }
                
                
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteUser(String id)
        {
            var user = await usermanger.FindByIdAsync(id);
            if(user == null)
            {
                return NotFound("user not found");
            }
            else
            {
                var result = await usermanger.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok("User updated successfully");
                }
                foreach (var i in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, i.Description);
                }
            }
            return BadRequest(ModelState);
            
            


        }

        //-------------
        [Authorize]
        [HttpGet("getorder")]
        public ActionResult getorder()
        {
            var userid =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userid == null)
            {
                return Unauthorized();
            }
            else
            {
                var order = context.Orders.Where(x => x.userid == userid).Select(x => new orderDTO
                {
                    id = x.Id,
                    ordernam = x.Name,
                    status = x.Status.ToString(),
                    totalprice = x.Totalprice

                }).ToList();
                return Ok(order);
            }
        }

    }
}
