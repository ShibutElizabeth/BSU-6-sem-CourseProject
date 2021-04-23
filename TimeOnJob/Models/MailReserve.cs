using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alia.Models
{
    public class MailReserve
    {
        public int? ItemId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ItemName { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}
