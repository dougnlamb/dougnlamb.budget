using dougnlamb.core.security;
using System;

namespace dougnlamb.budget.models {
    public class BudgetEditorModel : IBudgetEditorModel {

        private IBudget mBudget;

        public BudgetEditorModel(ISecurityContext securityContext, IUser user) {
            this.mBudget = new Budget();
            this.Owner = user.View(securityContext);
            this.Name = "";
            this.CurrencySelector = new CurrencySelectionModel(0);
        }

        public BudgetEditorModel(ISecurityContext securityContext, IBudget budget) {
            this.mBudget = budget;
            this.oid = budget.oid;
            this.Name = budget.Name;
//            this.Period = budget.Period;
            this.CurrencySelector = new CurrencySelectionModel(budget?.DefaultCurrency?.oid ?? 0);
        }

        public int oid { get; internal set; }
        public string Name { get; set; }
        public IUserViewModel Owner { get; set; }
        public IBudgetPeriodViewModel Period { get; set; }

        public int DefaultCurrencyId {
            get {
                return CurrencySelector.SelectedCurrencyId;
            }
        }

        public CurrencySelectionModel CurrencySelector { get; set; }

        public IBudget Save(ISecurityContext securityContext) {
            if (mBudget == null) {
                if (this.oid > 0) {
                    mBudget = Budget.GetDao().Retrieve(securityContext, this.oid);
                }
                else {
                    mBudget = new Budget();
                }
            }

            mBudget.Save(securityContext, this);

            return mBudget;
        }
    }
}