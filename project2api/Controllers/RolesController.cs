using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project2api.DTO;
using project2api.Model;

namespace project2api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser>userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpPost("addrole")]
        public async Task<ActionResult>AddRole(string rolename)
        {
            if(!string.IsNullOrEmpty(rolename))
            {
                // cheach role exist or not
                bool existrole= await roleManager.RoleExistsAsync(rolename);
                if (!existrole)
                {
                    var result= await roleManager.CreateAsync(new IdentityRole(rolename));
                    if (result.Succeeded)
                    {
                        return Ok("role added susfully");
                    }
                    else
                    {
                        BadRequest("falid to add role");
                    }
                }
               
            }
            return NotFound("this role alread exist");

        }
        [HttpPost("add userrole")]
        public async Task<ActionResult> AdduserRole(string emailuser,string role)
        {
            // cheack if the role exist or not 
            // cheach if emailuser exist or bot 

            var user = await userManager.FindByEmailAsync(emailuser);
            if(user == null)
            {
                return NotFound("this email not found");
            }
            bool roleexist= await roleManager.RoleExistsAsync(role);
            if (!roleexist)
            {
                return BadRequest("this role not exist");
            }
             var resule= await userManager.AddToRoleAsync(user,role);
            if (resule.Succeeded)
            {
                return Ok($"this user {emailuser} has role {role} ");
            }
            return BadRequest("something error");
        }

        // remove role from user 
        [HttpPost("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser(string userEmail, string roleName)
        {
            // Step 1: Find the user by their email
            var user = await userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            // Step 2: Check if the user has the role
            var isInRole = await userManager.IsInRoleAsync(user, roleName);
            if (!isInRole)
            {
                return BadRequest($"User is not in role '{roleName}'");
            }

            // Step 3: Remove the role from the user
            var result = await userManager.RemoveFromRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Ok($"Role '{roleName}' removed from user '{userEmail}' successfully");
            }

            return BadRequest("Failed to remove role from user");
        }

        // get all roles 
        [HttpGet("getallrouls")]

        public async  Task<ActionResult> getroles()
        {
            var roles =  roleManager.Roles.ToList();

            return Ok(roles);
            
        }
        [HttpGet("get role for spasific user")]

        public async Task<IActionResult> GetUsersWithRoles()
        {
            // List to store users and their roles
            var usersWithRoles = new List<userRole>();

            // Get all users
            var users = userManager.Users.ToList();

            foreach (var user in users)
            {
                // Get the roles for the current user
                var roles = await userManager.GetRolesAsync(user);

                // Add the user and their roles to the list
                usersWithRoles.Add(new userRole
                {
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return Ok(usersWithRoles);
        }







    }
}
