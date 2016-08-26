using dougnlamb.core;
using dougnlamb.core.collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface IBudget : IBaseObject {
        int oid { get; set; }
        IUser Owner { get; }

        string Name { get; }
        void UpdateName(string name, IUser user);

        IBudgetPeriod Period { get; }
        IObservable<IBudgetItem> BudgetItems { get; }

        IBudgetItem AddBudgetItem(IMoney amount, string name, string notes, IUser creatingUser);
        void AddUserAccess(IUser user, UserAccessMode accessMode);

        bool IsClosed { get; }
        void Close(IUser user);
    }
}
