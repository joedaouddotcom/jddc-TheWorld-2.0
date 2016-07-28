using jddc_TheWorld_2._0.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jddc_TheWorld_2._0.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController :Controller
    {
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repository)
        {
            _repository = repository;

        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllTrips());
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]ViewModels.TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                return Created($"api/trips/{theTrip.Name}", theTrip);
            }

            return BadRequest(ModelState);
            
        }
    }
}
