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

        IMoney Amount { get; }

        IMoney Balance { get; }

        IObservableList<IAllocation> Allocations { get; }

        string Notes { get; }

        DateTime ReminderDate { get; }

        DateTime DueDate { get; }

        bool IsClosed { get; }
        void Close(IUser user);

        DateTime ClosedDate { get; }
        IUser ClosedBy { get; }

        IBudgetItemEditorModel Edit(ISecurityContext securityContext);
        void Save(ISecurityContext securityContext, IBudgetItemEditorModel model);
    }
}
