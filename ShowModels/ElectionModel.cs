using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowModels
{
    public class TotalElection
    {
        public List<ElectionModel> Elec { get; set; }
    }
    public class ElectionModel
    {
        public int Id { get; set; }
        public string ElectionName { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}
