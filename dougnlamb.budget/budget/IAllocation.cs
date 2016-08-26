using dougnlamb.core;
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
        void UpdateAmount(ICurrency amount, IUser user);

        string Notes { get; }
        void UpdateNotes(string notes, IUser user);

        void UpdateAllocation(ICurrency amount, string notes, IUser user);

        IAllocationEditorModel Edit(IUser user);
        void Save(IUser user, IAllocationEditorModel model);
    }
}
