using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project2api.Data;
using project2api.DTO.emloyeesDto;
using project2api.DTO.ProductDto;
using project2api.Migrations;
using project2api.Model;
using project2api.Repositary;
using project2api.Repositary.interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace project2api.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class ProductController : ControllerBase
    {
        

        public ProductController(IProduct Repositary)
        {
            repositary = Repositary;
        }
        GenralRespnse genralrespnse = new GenralRespnse();
        private readonly IProduct repositary;


        //-------------------------- get All employees-----------------------------------
        [HttpGet]

        public ActionResult<GenralRespnse> getAlldata()
        {
            var data = repositary.GetAll();
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
         public async Task< ActionResult<GenralRespnse>> getEmpById(int id)
        {

            var data =await repositary.GetById(id);
            if (data == null)
            {
                genralrespnse.issucces = false;
                genralrespnse.data = $" the product for this id {id } dont exist pleas enter the valid id";
                
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
       
        public ActionResult<GenralRespnse> creatEmpyee([FromBody] productAddDTO product)
        {

        var   data = repositary.Add(product);
            if(data == null)
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
        
        public async Task<ActionResult> updateEmpyloyee( [FromBody] productDTO product)
        {

            var data =await repositary.Update( product);

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
            var data =await repositary.Remove(id);

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
        [HttpGet("getCategoryForSpasficProduc")]
        public async Task<ActionResult> getproductandspaficCategory(int id)
        {
            var data = await repositary.GetProductAndCategory(id);

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

