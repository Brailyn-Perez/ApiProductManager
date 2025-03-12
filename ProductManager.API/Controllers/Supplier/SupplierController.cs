using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManager.API.DTOS.Supplier;
using ProductManager.API.Entities;

namespace ProductManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SupplierController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetSuppliers()
        {
            var suppliers = await _context.Suppliers
                .Select(s => new SupplierDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Contact = s.Contact
                })
                .ToListAsync();

            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDTO>> GetSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound();

            return Ok(new SupplierDTO
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Contact = supplier.Contact
            });
        }

        [HttpPost]
        public async Task<ActionResult<SupplierDTO>> CreateSupplier(CreateOrUpdateSupplierDTO createDto)
        {
            var supplier = new Supplier
            {
                Name = createDto.Name,
                Contact = createDto.Contact
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSupplier), new { id = supplier.Id }, new SupplierDTO
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Contact = supplier.Contact
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, CreateOrUpdateSupplierDTO updateDto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound();

            supplier.Name = updateDto.Name;
            supplier.Contact = updateDto.Contact;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound();

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
