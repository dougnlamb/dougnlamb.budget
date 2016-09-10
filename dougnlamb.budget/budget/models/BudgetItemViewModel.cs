using System;
using dougnlamb.budget.models;
using dougnlamb.core.security;

namespace dougnlamb.budget {
    internal class BudgetItemViewModel : IBudgetItemViewModel {
        private ISecurityContext mSecurityContext;

        public BudgetItemViewModel(ISecurityContext securityContext, BudgetItem budgetItem) {
            this.mSecurityContext = securityContext;

            oid = budgetItem?.oid ?? 0;
            Name = budgetItem?.Name ?? "";
            Notes = budgetItem?.Notes ?? "";
            DueDate = budgetItem?.DueDate ?? new DateTime();
            ReminderDate = budgetItem?.ReminderDate ?? new DateTime();
            Amount = budgetItem?.Amount?.View(mSecurityContext) ?? new MoneyViewModel(mSecurityContext, null);
        }

        public IMoneyViewModel Amount { get; internal set; }
        public DateTime DueDate {get;internal set;}
        public string Name {get;internal set;}
        public string Notes {get;internal set;}
        public int oid {get;internal set;}
        public DateTime ReminderDate {get;internal set;}
    }
}