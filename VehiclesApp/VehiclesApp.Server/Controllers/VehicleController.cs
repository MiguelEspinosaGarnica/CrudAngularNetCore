using Microsoft.AspNetCore.Mvc;
using VehiclesApp.Server.Models;
using VehiclesApp.Server.Services;
using static VehiclesApp.Server.Services.VehicleService;

namespace VehiclesApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService _vehicleService;

        public VehicleController()
        {
            _vehicleService = new VehicleService();
        }

        // GET: api/vehicle
        [HttpGet]
        public ActionResult<PagedResponse<Vehicle>> Get(int pageIndex = 0, int pageSize = 5)
        {
            return Ok(_vehicleService.GetAllVehicles(pageIndex, pageSize));
        }

        // GET: api/vehicle/{id}
        [HttpGet("{id}")]
        public ActionResult<Vehicle> Get(int id)
        {
            var vehicle = _vehicleService.GetVehicleById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        // POST: api/vehicle
        [HttpPost]
        public ActionResult Post([FromBody] Vehicle vehicle)
        {
            _vehicleService.AddVehicle(vehicle);
            return Ok();
        }

        // PUT: api/vehicle/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Vehicle vehicle)
        {
            var existingVehicle = _vehicleService.GetVehicleById(id);
            if (existingVehicle == null)
            {
                return NotFound();
            }
            _vehicleService.UpdateVehicle(vehicle);
            return Ok();
        }

        // DELETE: api/vehicle/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var vehicle = _vehicleService.GetVehicleById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            _vehicleService.DeleteVehicle(id);
            return Ok();
        }
    }
}
