using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project2api.Data;
using project2api.DTO.AuthntcationDTO;
using project2api.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace project2api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> userManger;
        private readonly IConfiguration config;

        public AuthController(AppDbContext context,UserManager<AppUser> userManger,IConfiguration config)
        {
            this.context = context;
            this.userManger = userManger;
            this.config = config;
        }

        [HttpPost("Regster")]
        public async Task<ActionResult> Regstration(RegstrationDTO regdto)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.Email = regdto.Email;
                user.UserName = regdto.username;
             var result= await  userManger.CreateAsync(user, regdto.Password);
                if (result.Succeeded)
                {
                    var existcart = await context.Carts.FirstOrDefaultAsync(x => x.userid == user.Id);
                    if(existcart == null)
                    {
                        Cart cart = new Cart
                        {
                            userid = user.Id,
                            name = user.UserName

                        };
                        context.Carts.Add(cart);
                        await context.SaveChangesAsync();

                    }
                    else
                    {
                        return Ok("User already has a cart.");
                    }



                    return Ok("created");
                }
                else
                {
                    foreach(var i in result.Errors)
                    {
                        ModelState.AddModelError("password", i.Description);
                    }

                }

            }
            return BadRequest(ModelState);
            
        }
        [HttpPost("login")]
        public async Task<ActionResult>Login(LoginDTO logindto)
        {
            if (ModelState.IsValid)
            {
            AppUser user=await    userManger.FindByNameAsync(logindto.username);
                if (user != null)
                {
                    bool found = await userManger.CheckPasswordAsync(user, logindto.password);
                    if (found)
                    {


                        // ---------------do clims
                        List<Claim> userclaims = new List<Claim>();
                        //will add id and username and roles
                        userclaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        userclaims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        // ensur that token primary 
                        userclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        // this is the list get the all Roals for user (user can has the many roals)
                        var Roles = await userManger.GetRolesAsync(user);
                        foreach(var role in Roles)
                        {
                            userclaims.Add(new Claim(ClaimTypes.Role, role));
                        }
                        //------------------------ end clims

                        //------------------------- do signingCredentials

                        //(in the symmanticsercurykey will take the key but byte not string ) we will convert str => byte
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:secret"].PadRight(48)));

                        SigningCredentials _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha384);

                        // Design token (not genrate)
                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: config["JWT:valid_issur"],
                            audience: config["JWT:valdid_audiance"],
                            expires: DateTime.Now.AddHours(1),
                            claims: userclaims,
                            signingCredentials: _signingCredentials
                            );
                        return Ok(new
                        {
                            // genrate token 
                            token = new JwtSecurityTokenHandler().WriteToken(myToken),
                            expiration= myToken.ValidTo
                        });
                        
                    }
                }
                ModelState.AddModelError("username", "username or password invalid");

            }
           
            return BadRequest(ModelState);

        }
    }
}
