using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignDesignCorpus.ViewModels
{
    public class MaintenanceSectionParametersViewModel
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public int? AccountId { get; set; }
        public int? FundId { get; set; }
        public int? TaskId { get; set; }
        public int? PCBusId { get; set; }
        public int? ProjectId { get; set; }
        public int? ActivityId { get; set; }
        public int? ResTypeId { get; set; }
    }
}
