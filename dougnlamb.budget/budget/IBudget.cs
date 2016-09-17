using dougnlamb.budget.models;
using dougnlamb.core;
using dougnlamb.core.collections;
using dougnlamb.core.security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget {
    public interface IBudget : IBaseObject {
        int oid { get; }
        IUser Owner { get; }

        string Name { get; }

        IMoney PlannedBalance { get; }
        IMoney ActualBalance { get; }

        ICurrency DefaultCurrency { get; }
        IBudgetPeriod Period { get; }

        IObservableList<IBudgetItem> BudgetItems { get; }
        IBudgetItem AddBudgetItem(ISecurityContext securityContext, IBudgetItemEditorModel model);

        void AddUserAccess(IUser user, UserAccessMode accessMode);

        bool IsClosed { get; }

        IBudgetViewModel View(ISecurityContext mSecurityContext);
        void Save(ISecurityContext securityContext, IBudgetEditorModel model);
    }
}
