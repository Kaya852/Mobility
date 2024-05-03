using AspCrudApi.Data;
using AspCrudApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;

namespace AspCrudApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecordsController : Controller
    {

        // Dependency Injection

        private readonly AspCrudApiDbContext _dbContext;  // Implementation of ContactsAPIDbContext is done internally

        
        public RecordsController(AspCrudApiDbContext dbContext) // Constructor
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> list() // List all records
        {
            try
            {
                var records = await _dbContext.Record.ToListAsync();
                return Ok(records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpGet("{name}")]
        public async Task<IActionResult> search([FromRoute] string name) // List people with given name
        {
            var records = await _dbContext.Record.Where(r => r.Name.Contains(name)).ToListAsync();

            if (records == null || records.Count == 0)
            {
                return NotFound();
            }

            return Ok(records);
        }

        [HttpPost]
        public async Task<IActionResult> add([FromBody] AddRecordContextRequest addContextRequest)  // Add record
        {
            var record = new Record()
            {

                Name = addContextRequest.Name,
                Surname = addContextRequest.Surname,
                Age = addContextRequest.Age,

            };
            await _dbContext.Record.AddAsync(record);
            await _dbContext.SaveChangesAsync();

            return Ok(record);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id) // Delete by ID
        {
            var contact = await _dbContext.Record.FindAsync(id);

            if (contact != null)
            {

                _dbContext.Remove(contact);
                await _dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }




    }
}
