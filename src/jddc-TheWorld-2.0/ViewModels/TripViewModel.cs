﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jddc_TheWorld_2._0.ViewModels
{
    public class TripViewModel
    {
        [Required]
        [StringLength(100, MinimumLength =5)]
        public string Name { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}