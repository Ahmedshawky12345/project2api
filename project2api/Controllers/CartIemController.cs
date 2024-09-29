using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project2api.Data;
using project2api.DTO;
using project2api.DTO.CateoryDTO;
using project2api.Repositary.interfaces;

namespace project2api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartIemController : ControllerBase
    {
        public CartIemController(ICartitem repsitory)
        {
            this.repsitory = repsitory;
        }
        GenralRespnse genralresponse = new GenralRespnse();
        private readonly ICartitem repsitory;

        [HttpPost]

        public ActionResult<GenralRespnse> AddCategory(CartiemDTO cartitemdto)
        {
            var data = repsitory.Add(cartitemdto);
            if (data == null)
            {
                genralresponse.issucces = false;
                genralresponse.data = "no data , error eccoure";
                return Ok(genralresponse);
            }
            else
            {
                genralresponse.issucces = true;
                genralresponse.data = data;
                return Ok(genralresponse);
            }
        }
        // getby id 
        [HttpGet("byid")]
        public async Task<ActionResult<GenralRespnse>> getEmpById(int id)
        {

            var data = await repsitory.GetById(id);
            if (data == null)
            {
                genralresponse.issucces = false;
                genralresponse.data = $" the cartitem for this id {id} dont exist pleas enter the valid id";

                return Ok(genralresponse);
            }
            else
            {
                genralresponse.issucces = true;
                genralresponse.data = data;

                return Ok(genralresponse);


            }


        }
        [HttpGet]

        public ActionResult<GenralRespnse> getAllCategory()
        {
            var data = repsitory.GetAll();
            if (data == null)
            {
                genralresponse.issucces = false;
                genralresponse.data = "no data , error eccoure";
                return Ok(genralresponse);
            }
            else
            {
                genralresponse.issucces = true;
                genralresponse.data = data;
                return Ok(genralresponse);
            }




        }
        //------------------------ update employee-------------------------------
        [HttpPut]

        public async Task<ActionResult> updateEmpyloyee([FromBody] CartiemDTO cartitemdto)
        {

            var data = await repsitory.Update(cartitemdto);

            if (data == null)
            {
                genralresponse.issucces = false;
                genralresponse.data = "no data , error eccoure";
                return Ok(genralresponse);
            }
            else
            {
                genralresponse.issucces = true;
                genralresponse.data = data;
                return Ok(genralresponse);
            }

        }
        [HttpDelete]
        public async Task<ActionResult<GenralRespnse>> Delete(int id)
        {
            var data = await repsitory.Remove(id);

            if (data == null)
            {
                genralresponse.issucces = false;
                genralresponse.data = "no data , error eccoure";
                return Ok(genralresponse);
            }
            else
            {
                genralresponse.issucces = true;
                genralresponse.data = data;
                return Ok(genralresponse);
            }

        }
    }
}
