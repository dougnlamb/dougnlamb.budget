using dougnlamb.core;
using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface ITransaction : IBaseObject {
        int oid { get; }

        DateTime TransactionDate { get; }
        IMoney TransactionAmount { get; }

        IObservable<IAllocation> Allocations { get; }
        IAllocation CreateAllocation(IBudgetItem budgetItem, IMoney amount);

        IUser ReportedBy { get;  }
        DateTime ReportedDate { get; }

        string Note { get; set; }

        bool IsAllocated { get; }
        IMoney GetAllocationDiscrepency();

        bool IsCleared { get; }
        void ReportCleared(IUser user);
        void ReportCleared(IMoney updatedAmount, IUser user);
        DateTime ClearedDate { get; }

        ITransactionEditorModel Edit(ISecurityContext securityContext);
        void Save(ISecurityContext securityContext, ITransactionEditorModel model);
    }
}
