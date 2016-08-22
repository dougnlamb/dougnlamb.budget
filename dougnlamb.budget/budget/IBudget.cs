using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface IBudget {
        int oid { get; set; }
        IUser Owner { get; set; }
        string Name { get; set; }

        IBudgetPeriod Period { get; set; }
        IBudgetItem[] BudgetItems { get; set; }

        bool IsClosed { get; set; }
    }
}
