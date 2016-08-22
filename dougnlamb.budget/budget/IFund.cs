using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dougnlamb.budget {
    public interface IFund {
        string Name { get; set; }
        Decimal Balance { get; set; }
        Decimal Goal { get; set; }
        DateTime TargetDate { get; set; }
        int oid { get; set; }
    }
}