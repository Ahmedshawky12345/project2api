using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project2api.Data;
using project2api.DTO.OrderDTO;
using project2api.DTO.OrderitemDTO;
using project2api.Repositary.interfaces;

namespace project2api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderitemController : ControllerBase
    {
        GenralRespnse genralrespnse = new GenralRespnse();

        private readonly IOrderItem repsitory;

        public OrderitemController(IOrderItem repsitory)
        {
            this.repsitory = repsitory;
        }


        //-------------------------- get All orderitem-----------------------------------
        [HttpGet]

        public ActionResult<GenralRespnse> getAlldata()
        {
            var data = repsitory.GetAll();
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
        //-------------------------------------- get spacific orderiem-----------------------
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

        // --------------------------- create orderitem ---------------------------------------
        [HttpPost]

        public ActionResult<GenralRespnse> creatEmpyee([FromBody] Orderitemadd orderitemdto)
        {

            var data = repsitory.Add(orderitemdto);
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

        //------------------------ update orderitem-------------------------------
        [HttpPut]

        public async Task<ActionResult> updateEmpyloyee([FromBody] orderitemDTO orderitemdto)
        {

            var data = await repsitory.Update(orderitemdto);

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
        //---------------------- Delete orderitem------------------------
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

    }
}
