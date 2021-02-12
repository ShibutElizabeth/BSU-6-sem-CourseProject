using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alia.Models
{
    public class Region
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public ICollection<Locality> Localities { get; set; }
    }
}
