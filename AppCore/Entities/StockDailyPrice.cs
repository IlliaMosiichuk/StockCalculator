﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities
{
    public class StockDailyPrice : BaseEntity
    {
        public string Symbol { get; set; }

        public DateTime Date { get; set; }

        public decimal ClosePrice { get; set; }
    }
}
