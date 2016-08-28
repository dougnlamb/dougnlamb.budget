using dougnlamb.core;
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
        void UpdateName(string name, IUser user);

        IBudget Budget { get; }

        IMoney Amount { get; }
        void UpdateAmount(IMoney amount, IUser user);

        IMoney Balance { get; }

        IObservable<IAllocation> Allocations { get; }

        string Notes { get; }
        void UpdateNotes(string notes, IUser user);

        DateTime ReminderDate { get; }
        void UpdateReminderDate(DateTime reminderDate, IUser user);

        DateTime DueDate { get; }
        void UpdateDueDate(DateTime dueDate, IUser user);

        bool IsClosed { get; }
        void Close(IUser user);

        DateTime ClosedDate { get; }
        IUser ClosedBy { get; }

        IBudgetItemEditorModel Edit(ISecurityContext securityContext);
        void Save(ISecurityContext securityContext, IBudgetItemEditorModel model);
    }
}
