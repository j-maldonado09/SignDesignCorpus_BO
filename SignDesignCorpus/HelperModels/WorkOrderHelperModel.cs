using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignDesignCorpus.HelperModels
{
    public class WorkOrderHelperModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int MaterialRequestedFromId { get; set; }
        public int MaterialRequestedById { get; set; }
        public int DepartmentId { get; set; }
        public int AccountId { get; set; }
        public int FY { get; set; }
        public int FundId { get; set; }
        public int? TaskId { get; set; }
        public int PCBusId { get; set; }
        public int ProjectId { get; set; }
        public int ActivityId { get; set; }
        public int ResTypeId { get; set; }
        public string RequestedByMaintenanceId { get; set; }
        public DateTime RequestedByMaintenanceDate { get; set; }
        public string? ApprovedByMaintenanceId { get; set; }
        public DateTime? ApprovedByMaintenanceDate { get; set; }
		public string? ApprovedByAreaEngineerId { get; set; }
		public DateTime? ApprovedByAreaEngineerDate { get; set; }
		public string? ApprovedByDistrictId { get; set; }
        public DateTime? ApprovedByDistrictDate { get; set; }
        public DateTime? SignReceivedDate { get; set; }
        public DateTime? SignInstalledDate { get; set; }

        ////public int SignShop { get; set; }
        public List<WorkOrderItemHelperModel> Items { get; set; }
    }
}
