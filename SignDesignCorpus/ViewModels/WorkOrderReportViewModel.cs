using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignDesignCorpus.ViewModels
{
    public class WorkOrderReportViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string OrderNumber { get; set; }
        public int MaterialRequestedFromId { get; set; }
        public string MaterialRequestedFromName { get; set; }
        public int MaterialRequestedById { get; set; }
        public string MaterialRequestedByName { get; set; }
        public string MaterialRequestedByNumber { get; set; }
        //public int DepartmentId { get; set; }
        //public int AccountId { get; set; }
        //public int FY { get; set; }
        //public int FundId { get; set; }
        //public int TaskId { get; set; }
        //public int PCBusId { get; set; }
        //public int ProjectId { get; set; }
        //public int ActivityId { get; set; }
        //public int ResTypeId { get; set; }
        //public int RequestedByMaintenanceId { get; set; }

        public string Status { get; set; }
        public DateTime StatusDate { get; set; }
        //public DateTime RequestedByMaintenanceDate { get; set; }
        //public int ApprovedByMaintenanceId { get; set; }
        //public DateTime ApprovedByMaintenanceDate { get; set; }
        //public int ApprovedByDistrictId { get; set; }
        //public DateTime ApprovedByDistrictDate { get; set; }
        ////public int SignShop { get; set; }
        //public List<WorkOrderItemHelperModel> Items { get; set; }
    }
}
