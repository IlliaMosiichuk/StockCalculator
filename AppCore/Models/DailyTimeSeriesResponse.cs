using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Models
{
    public class DailyTimeSeriesResponse
    {
        public List<DailyTimeSeriesResponseItem> Items { get; set; } = new List<DailyTimeSeriesResponseItem>();
    }

    public class DailyTimeSeriesResponseItem
    {
        public DateTime Date { get; set; }

        public decimal ClosePrice { get; set; }
    }
}
