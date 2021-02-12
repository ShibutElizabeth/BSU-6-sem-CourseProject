using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alia.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string UserId { get; set; }
        public int LocalityId { get; set; }
        public int CategoryId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FileName { get; set; }
        public byte[] OriginalImageData { get; set; }
        public byte[] BigImageData { get; set; }
        public byte[] PreviewImageData { get; set; }
        public decimal? Price { get; set; }
        public Locality Locality { get; set; }
        //public Region Region { get; set; }
        public Category Category { get; set; }
    }
}
