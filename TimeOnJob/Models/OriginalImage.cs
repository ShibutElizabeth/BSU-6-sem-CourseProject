using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alia.Models
{
    public class OriginalImage
    {
        public int OriginalImageId { get; set; }
        
        public string FileName { get; set; }
        
        public byte[] OriginalImageData { get; set; }
    }
}
