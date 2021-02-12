using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alia.ViewModels;

namespace Alia.Models
{
    public class SortViewModel
    {
        public SortState DateSort { get; set; }
        public SortState State { get; set; }
        public SortState PriceSort { get; set; }
        public SortState Current { get; set; }
        public bool UpPrice { get; set; }
        public bool UpDate { get; set; }
        public SortViewModel(SortState sortOrder)
        {
            // значения по умолчанию
            UpDate = true;
            UpPrice = false;
            DateSort = SortState.DateAsc;
            PriceSort = SortState.PriceDesc;

            State = sortOrder;
            switch (sortOrder)
            {
                case SortState.DateDesc:
                    Current = DateSort = SortState.DateAsc;
                    UpDate = true;
                    UpPrice = false;
                    break;
                case SortState.PriceDesc:
                    Current = PriceSort = SortState.PriceAsc;
                    UpPrice = true;
                    UpDate = false;
                    break;
                case SortState.PriceAsc:
                    Current = PriceSort = SortState.PriceDesc;
                    UpPrice = false;
                    UpDate = false;
                    break;
                case SortState.DateAsc:
                    Current = DateSort = SortState.DateDesc;
                    UpDate = false;
                    UpPrice = false;
                    break;
                
            }
        }
       /*
       public SortViewModel(SortState sortOrder)
        {
            DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            SalarySort = sortOrder == SortState.SalaryAsc ? SortState.SalaryDesc : SortState.SalaryAsc;
            Current = sortOrder = SortState.DateDesc;
        }*/
    }
}
