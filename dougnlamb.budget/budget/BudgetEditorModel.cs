using dougnlamb.core.security;
using System;

namespace dougnlamb.budget {
    internal class BudgetEditorModel : IBudgetEditorModel {

        private IBudget mBudget;
        public BudgetEditorModel(ISecurityContext securityContext, Budget budget) {
            this.mBudget = budget;
            this.oid = budget.oid;
            this.Name = budget.Name;
            this.Period = budget.Period;
        }

        public int oid { get; internal set; }
        public string Name { get; set; }
        public IBudgetPeriod Period { get; set; }

        public IBudget Save(ISecurityContext securityContext) {
            if(mBudget == null) {
                mBudget = Budget.GetDao().Retrieve(securityContext, this.oid);
            }
            mBudget.Save(securityContext, this);

            return mBudget;
        }
    }
}