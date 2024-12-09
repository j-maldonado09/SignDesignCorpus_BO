using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignDesignCorpus.HelperModels
{
    public class WorkOrderNamesHelperModel
    {
        //public int Id { get; set; }
        //public string Number { get; set; }
        public string MaterialRequestedFromName { get; set; }
        public string MaterialRequestedByName { get; set; }
        public string MaterialRequestedByNumber { get; set; }
        public string MaterialRequestedByAddress { get; set; }
        public string MaterialRequestedByCity { get; set; }
        public string MaterialRequestedByState { get; set; }
        public string MaterialRequestedByZipCode { get; set; }
        public string MaterialRequestedByEmail { get; set; }
        public string DepartmentName { get; set; }
        public string AccountName { get; set; }
        //public int FY { get; set; }
        public string FundName { get; set; }
        public string TaskName { get; set; }
        public string PCBusName { get; set; }
        public string ProjectName { get; set; }
        public string ActivityName { get; set; }
        public string ResTypeName { get; set; }
        public string RequestedByMaintenanceName { get; set; }
        //public DateTime RequestedByMaintenanceDate { get; set; }
        public string ApprovedByMaintenanceName { get; set; }
        //public DateTime ApprovedByMaintenanceDate { get; set; }
        public string ApprovedByDistrictName { get; set; }
        //public DateTime ApprovedByDistrictDate { get; set; }
        ////public int SignShop { get; set; }
        //public List<WorkOrderItemHelperModel> Items { get; set; }
    }
}
