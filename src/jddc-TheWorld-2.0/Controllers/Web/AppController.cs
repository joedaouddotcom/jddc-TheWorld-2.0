using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jddc_TheWorld_2._0.ViewModels;
using jddc_TheWorld_2._0.Services;
using Microsoft.Extensions.Configuration;
using jddc_TheWorld_2._0.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace jddc_TheWorld_2._0.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private WorldContext _context;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailService, IConfigurationRoot config, IWorldRepository repository, ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }


        public IActionResult Index()
        {
            
            return View();
            
        }

        [Authorize]
        public IActionResult Trips()
        {
             var trips = _repository.GetAllTrips();
             return View(trips);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("Email", "We don't support aol.com addresses");
            }

            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From jddc-TheWorld-2.0", model.Message);

                ModelState.Clear();

                ViewBag.UserMessage = "Message Sent!";
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
