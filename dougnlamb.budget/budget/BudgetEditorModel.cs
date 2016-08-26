using System;

namespace dougnlamb.budget {
    internal class BudgetEditorModel : IBudgetEditorModel {

        public BudgetEditorModel(IUser editingUser, Budget budget) {
            this.oid = budget.oid;
            this.Name = budget.Name;
            this.Period = budget.Period;
        }

        public int oid { get; internal set; }
        public string Name { get; set; }
        public IBudgetPeriod Period { get; set; }
    }
}