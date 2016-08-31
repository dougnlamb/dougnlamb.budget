using dougnlamb.core;
using dougnlamb.core.collections;
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

        IObservableList<IAllocation> Allocations { get; }
        IAllocationEditorModel CreateAllocation(ISecurityContext securityContext);
        IAllocation AddAllocation(ISecurityContext securityContext, IAllocationEditorModel model);

        IUser ReportedBy { get;  }
        DateTime ReportedDate { get; }

        string Note { get; set; }

        bool IsAllocated { get; }
        IMoney GetAllocationDiscrepency();

        bool IsCleared { get; }
        DateTime ClearedDate { get; }

        ITransactionEditorModel Edit(ISecurityContext securityContext);
        void Save(ISecurityContext securityContext, ITransactionEditorModel model);
    }
}
