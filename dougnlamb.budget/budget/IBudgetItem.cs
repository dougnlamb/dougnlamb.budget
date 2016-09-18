using dougnlamb.budget.models;
using dougnlamb.core;
using dougnlamb.core.collections;
using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface IBudgetItem : IBaseObject {
        int oid { get; }
        string Name { get; }

        IBudget Budget { get; }
        IAccount DefaultAccount { get; }

        IMoney BudgetAmount { get; }
        IMoney Balance { get; }

        IObservableList<IAllocation> Allocations { get; }

        string Notes { get; }

        DateTime ReminderDate { get; }
        DateTime DueDate { get; }

        bool IsClosed { get; }

        DateTime ClosedDate { get; }
        IUser ClosedBy { get; }

        IBudgetItemViewModel View(ISecurityContext mSecurityContext);
        void Save(ISecurityContext securityContext, IBudgetItemEditorModel model);
    }
}
