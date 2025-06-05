using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignDesignCorpus.HelperModels;

namespace SignDesignCorpus.ViewModels
{
    public class WorkOrderViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int MaterialRequestedFromId { get; set; }
        public string MaterialRequestedFromName { get; set; }
        public int MaterialRequestedById { get; set; }
        public string MaterialRequestedByName { get; set; }
        public string MaterialRequestedByNumber { get; set; }
        //public bool IsHold { get; set; }
        public string Status { get; set; }
        public DateTime StatusDate { get; set; }
    }
}
