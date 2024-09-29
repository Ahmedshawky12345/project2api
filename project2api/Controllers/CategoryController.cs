using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project2api.Data;
using project2api.DTO;
using project2api.DTO.CateoryDTO;
using project2api.DTO.emloyeesDto;
using project2api.Repositary.interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace project2api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
       
        public CategoryController(ICategory repsitory)
        {
            this.repsitory = repsitory;
        }
        GenralRespnse genralresponse = new GenralRespnse();
        private readonly ICategory repsitory;


        [Authorize(Roles ="Admin")]
        [HttpPost]

        public  ActionResult< GenralRespnse> AddCategory(categorDTOforAdd categoryDto)
        {
            var data =  repsitory.Add(categoryDto);
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

        [HttpGet]
        [Authorize(Roles = "User")]
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

        public async Task<ActionResult> updateEmpyloyee([FromBody] CategoryDTO category)
        {

            var data = await repsitory.Update(category);

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





















        //[HttpGet]
        //public IActionResult allDepartmentsAndemps()
        //{
        //    var categoryies = context.categories.Include(x => x.employees).ToList();

        //    var departments = categoryies.Select(category => new CategoryDTO
        //    {
        //        id = category.Id,
        //        name = category.Name,
        //        manger= category.Mangsername,
        //        ProductsNames = category.employees.Select(dept_emp => new products
        //        {
        //            name = dept_emp.name
        //        }).ToList()
        //    }).ToList();

        //    return  Ok(departments);
        //}

    }
}
