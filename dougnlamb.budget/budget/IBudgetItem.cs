using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface IBudgetItem {
        Decimal Amount { get; set; }
        Decimal Balance { get; set; }

        string Notes { get; set; }
        string Name { get; set; }
        int oid { get; set; }
    }
}
