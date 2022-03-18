﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public interface ITickerCollected
    {
        public string BrokerName { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public string Volume { get; set; }
        public long TimeStamp { get; set; }
    }
}
