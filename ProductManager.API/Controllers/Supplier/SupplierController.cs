using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManager.BL.DTOS.Supplier;
using ProductManager.BL.Interfaces.Supplier;

namespace ProductManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var suppliers = _supplierService.GeAll();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var supplier = _supplierService.GeById(id);
            return Ok(supplier);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateOrUpdateSupplierDTO supplier)
        {
            _supplierService.Save(supplier);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateSupplierDTO supplier)
        {
            supplier.Id = id;
            _supplierService.Update(supplier);
            return Ok();
        }
    }
}
