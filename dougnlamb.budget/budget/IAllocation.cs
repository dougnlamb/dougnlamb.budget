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
        int oid { get; set; }
        ITransaction Transaction { get; }
        IBudgetItem BudgetItem { get; }

        ICurrency Amount { get; }

        string Notes { get; }

        IAllocationEditorModel Edit(ISecurityContext securityContext);
        void Save(ISecurityContext securityContext, IAllocationEditorModel model);
    }
}
