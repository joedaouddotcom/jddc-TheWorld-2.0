using AutoMapper;
using jddc_TheWorld_2._0.Models;
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
    [Route("api/trips")]
    public class TripsController :Controller
    {
        private ILogger<TripsController> _logger;
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;

        }

        [HttpGet("")]
        public JsonResult Get()
        {
            //try
            //{
                var trips = _repository.GetUserTripsWithStops(User.Identity.Name);
                var results = Mapper.Map<IEnumerable<TripViewModel>> (trips);

                return Json(results);
            //}
            //catch (Exception ex)
            //{
            //    // TODO Logging
            //    _logger.LogError($"Failed to get All Trips: {ex}");
            //    return BadRequest("Error Occurred");
            //}

        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                                   
                      
                    var newTrip = Mapper.Map<Trip>(vm);
                    newTrip.UserName = User.Identity.Name;

                    // Save to the Database
                    _logger.LogInformation("Attempting to save a new trip...");
                    _repository.AddTrip(newTrip);
                    
                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                        //return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new trip!", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
            
        }
    }
}
