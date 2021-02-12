using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alia.Models
{
    public class Locality
    {
        public int LocalityId { get; set; }
        public string LocalityName { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }//for adding products to recipe
    }
}
