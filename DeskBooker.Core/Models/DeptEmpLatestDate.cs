using System;
using System.Collections.Generic;

namespace DeskBooker.Core.Models
{
    public partial class DeptEmpLatestDate
    {
        public int EmpNo { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
