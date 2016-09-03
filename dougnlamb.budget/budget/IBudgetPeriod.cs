using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dougnlamb.budget {
    public interface IBudgetPeriod {
        int oid { get; set; }
        string Name { get; set; }

        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
    }
}