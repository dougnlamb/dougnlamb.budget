using dougnlamb.core.security;
using System;

namespace dougnlamb.budget.models {
    public class BudgetEditorModel : IBudgetEditorModel {

        private ISecurityContext mSecurityContext;
        private IBudget mBudget;

        public BudgetEditorModel(ISecurityContext securityContext, IUser user) {
            this.mSecurityContext = securityContext;
            this.mBudget = new Budget(securityContext);
            this.Owner = user;
            this.Name = "";
            this.DefaultCurrencySelector = new CurrencySelectionModel(securityContext, user?.DefaultCurrency);
        }

        public BudgetEditorModel(ISecurityContext securityContext, IBudget budget) {
            this.mSecurityContext = securityContext;
            this.mBudget = budget;
            this.oid = budget.oid;
            this.Name = budget.Name;
            this.Owner = budget.Owner;
            //            this.Period = budget.Period;
            this.DefaultCurrencySelector = new CurrencySelectionModel(securityContext, budget?.DefaultCurrency);
        }

        public int oid { get; internal set; }
        public string Name { get; set; }
        public IUser Owner { get; set; }
        public IBudgetPeriodViewModel Period { get; set; }

        public CurrencySelectionModel DefaultCurrencySelector { get; set; }

        IBudgetPeriod IBudgetEditorModel.Period {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public ICurrency DefaultCurrency {
            get {
                return DefaultCurrencySelector.SelectedCurrency;
            }
            set {
                DefaultCurrencySelector.SelectedCurrencyCode = value?.oid ?? 0;
            }
        }

        public IBudget Save(ISecurityContext securityContext) {
            if (mBudget == null) {
                if (this.oid > 0) {
                    mBudget = Budget.GetDao().Retrieve(securityContext, this.oid);
                }
                else {
                    mBudget = new Budget(securityContext);
                }
            }

            mBudget.Save(securityContext, this);

            return mBudget;
        }
    }
}