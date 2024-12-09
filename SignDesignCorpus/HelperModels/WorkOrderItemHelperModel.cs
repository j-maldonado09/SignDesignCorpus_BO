using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignDesignCorpus.HelperModels
{
    public class WorkOrderItemHelperModel
    {
        public int ItemId { get; set; }
        public string NIGP { get; set; }
        public bool RDC { get; set; }
        public int Quantity { get; set; }
        public string SignImage { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public bool Rush { get; set; }
        public bool New { get; set; }
        public string SpecialInstructions { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string? SignAttachment { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? InstalledDate { get; set; }
        public string? InstalledImage { get; set; }
    }
}
