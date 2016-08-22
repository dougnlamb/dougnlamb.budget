using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface ITransaction {
        DateTime TransactionDate { get; set; }
        Decimal TransactionAmount { get; set; }
        IBudgetItem[] BudgetItems { get; set; }

        IUser ReportedBy { get; set; }
        DateTime ReportedDate { get; set; }
        int oid { get; set; }
    }
}
