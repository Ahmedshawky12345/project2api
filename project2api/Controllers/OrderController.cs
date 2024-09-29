using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project2api.Data;
using project2api.DTO.emloyeesDto;
using project2api.DTO.OrderDTO;
using project2api.Model;
using project2api.Repositary.interfaces;
using System.Security.Claims;

namespace project2api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder repsitory;
        private readonly UserManager<AppUser> usermanger;

        public OrderController(IOrder repsitory,UserManager<AppUser>usermanger)
        {
            this.repsitory = repsitory;
            this.usermanger = usermanger;
        }
        GenralRespnse genralrespnse = new GenralRespnse();


        //-------------------------- get All employees-----------------------------------
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public ActionResult<GenralRespnse> getAlldata()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = repsitory.GetAll(userid);
            if (data == null)
            {
                genralrespnse.issucces = false;
                genralrespnse.data = "no data , error eccoure";
                return Ok(genralrespnse);
            }
            else
            {
                genralrespnse.issucces = true;
                genralrespnse.data = data;
                return Ok(genralrespnse);
            }




        }
        //-------------------------------------- get spacific employee-----------------------
        [HttpGet("byid")]
        public async Task<ActionResult<GenralRespnse>> getEmpById(int id)
        {

            var data = await repsitory.GetById(id);
            if (data == null)
            {
                genralrespnse.issucces = false;
                genralrespnse.data = "data not found";
                return Ok(genralrespnse);
            }
            else
            {
                genralrespnse.issucces = true;
                genralrespnse.data = data;

                return Ok(genralrespnse);


            }


        }

        // --------------------------- create employee ---------------------------------------
        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult<GenralRespnse> creatorder([FromBody] OrderAddDTO orderdto)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = repsitory.Add(orderdto,userid);
            if (data == null)
            {
                genralrespnse.issucces = false;
                genralrespnse.data = data;
                return Ok(genralrespnse);
            }
            else
            {
                genralrespnse.issucces = true;
                genralrespnse.data = data;
                return Ok(genralrespnse);
            }

        }

        //------------------------ update employee-------------------------------
        [HttpPut]

        public async Task<ActionResult> updateEmpyloyee([FromBody] orderDTO orderdto)
        {

            var data = await repsitory.Update(orderdto);

            if (data == null)
            {
                genralrespnse.issucces = false;
                genralrespnse.data = data;
                return Ok(genralrespnse);
            }
            else
            {
                genralrespnse.issucces = true;
                genralrespnse.data = data;
                return Ok(genralrespnse);
            }

        }
        //---------------------- Delete employee------------------------
        [HttpDelete]
        public async Task<ActionResult<GenralRespnse>> Delete(int id)
        {
            var data = await repsitory.Remove(id);

            if (data == null)
            {
                genralrespnse.issucces = false;
                genralrespnse.data = "error to get data";
                return Ok(genralrespnse);
            }
            else
            {
                genralrespnse.issucces = true;
                genralrespnse.data = data;
                return Ok(genralrespnse);
            }
        }

      

        // do order for login user 
    }
}
