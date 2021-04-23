using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alia.Models;

namespace Alia.ViewModels
{
    public class ItemReserveViewModel
    {
        public Item Item { get; set; }
        public AspNetUser User { get; set; }
        public MailReserve MailReserve { get; set; }
        
    }
}
