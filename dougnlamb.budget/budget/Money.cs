using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public class Money : IMoney {
        public decimal Amount { get; set; }
        public ICurrency Currency { get; set; }
    }
}
