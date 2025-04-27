using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using safriltest2_entitydata.Models;
using System;
using System.Diagnostics;

namespace safriltest2_entitydata.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            try
            { 
                var data = _context.TestingName.ToList();
                ViewBag.TestingName = data;

                return View();
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { message = "An Error occured while load data", error = ex.Message });
            }
        }

        

        [HttpPost]
        public IActionResult AddData([FromBody] TestingNameModel testingNameModel)
        {
            try
            {
                testingNameModel.IsActive = "1";
                testingNameModel.CreatedDate = DateTime.Now;
                testingNameModel.CreatedBy = "Safril";
                testingNameModel.UpdatedDate = DateTime.Now;
                testingNameModel.UpdatedBy = "Safril";

                _context.TestingName.Add(testingNameModel);
                _context.SaveChanges();
                return Ok(new { message = "Add Data successfully!" });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { Message = "An error occured while adding data.", Error = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult EditData([FromBody] TestingNameModel testingNameModel)
        {
            try
            {
                var data = _context.TestingName.Find(testingNameModel.Id);

                if (data == null)
                {
                    return NotFound(new { message = "Data Not Found!" });
                }

                data.Name = testingNameModel.Name;
                data.UpdatedDate = DateTime.Now;
                data.UpdatedBy = "Safril";
                _context.SaveChanges();

                return Ok(new { message = "Edit Data successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while edit data.", error = ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult DeleteData(int id)
        {
            try
            {
                var deleteData = _context.TestingName.Find(id);

                if (deleteData == null)
                {
                    return NotFound(new { message = "Data Not Found!" });
                }

                _context.TestingName.Remove(deleteData);
                _context.SaveChanges();

                return Ok(new { message = "Delete Successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while delete data. ", error = ex.Message });
            }
        }
    }
}
