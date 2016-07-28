using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace jddc_TheWorld_2._0.Services
{
    public class GeoCoordsService
    {
        private ILogger<GeoCoordsService> _logger;

        public GeoCoordsService(ILogger<GeoCoordsService> logger)
        {
            _logger = logger;
        }
          
        public async Task<GeoCoordsResult> GetCoordsAsync(string name)
        {
            var result = new GeoCoordsResult()
            {
                Success = false,
                Message = "Failed to get coordinates"
            };

            var apiKey = "";
            var encodedName = WebUtility.UrlEncode(name);
            var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={apiKey}";


        }
    }
}
