using dougnlamb.budget.models;
using dougnlamb.core;
using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface IAllocation : IBaseObject {
        int oid { get; }
        ITransaction Transaction { get; }
        IBudgetItem BudgetItem { get; }

        IMoney Amount { get; }

        string Notes { get; }

        void Save(ISecurityContext securityContext, IAllocationEditorModel model);
    }
}
