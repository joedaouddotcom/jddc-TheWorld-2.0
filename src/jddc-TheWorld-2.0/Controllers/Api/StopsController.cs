using AutoMapper;
using jddc_TheWorld_2._0.Models;
using jddc_TheWorld_2._0.Services;
using jddc_TheWorld_2._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace jddc_TheWorld_2._0.Controllers.Api
{
    [Authorize]
    [Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private GeoCoordsService _coordsService;
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;

        public StopsController(IWorldRepository repository, 
            ILogger<StopsController> logger, 
            GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var results = _repository.GetTripByName(tripName, User.Identity.Name);

                if (results == null)
                {
                    return Json(null);
                }

                //return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));
                return Json(Mapper.Map<IEnumerable<StopViewModel>>(results.Stops.OrderBy(s => s.Order)));
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to get stops: {0}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error occurred finding trip name");
            }

        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                // If the VM is valid
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(vm);

                    // Lookup the Geocodes

                    var coordResult = await _coordsService.GetCoordsAsync(newStop.Name);

                    if (!coordResult.Success)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Json(coordResult.Message);
                    }
                    else
                    {
                        newStop.Latitude = coordResult.Latitude;
                        newStop.Longitude = coordResult.Longitude;
                           
                        // Save to the Database

                        _repository.AddStop(tripName, User.Identity.Name, newStop);

                        if (_repository.SaveAll())
                        {
                            Response.StatusCode = (int)HttpStatusCode.Created;
                            //return Created($"/api/trips/{tripName}/stops/{newStop.Name}",
                            return Json(Mapper.Map<StopViewModel>(newStop));
                        }
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new Stop: {0}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to save new stop!");
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation failed to save new stop!");
        }
    }
}
