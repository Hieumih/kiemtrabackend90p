using System;
using System.Collections.Generic;

namespace CRM_CMC.Models
{
    public partial class Location
    {
        public int LocationId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
