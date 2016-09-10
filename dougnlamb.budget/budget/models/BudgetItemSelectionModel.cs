using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.models {
    public class BudgetItemSelectionModel : IBudgetItemSelectionModel {
        private ISecurityContext mSecurityContext;

        public BudgetItemSelectionModel() { }
        public BudgetItemSelectionModel(ISecurityContext securityContext) {
            this.mSecurityContext = securityContext;
        }

        private IList<IBudgetItemViewModel> mBudgetItems;
        public IList<IBudgetItemViewModel> BudgetItems {
            get {
                if (mBudgetItems == null) {
                    IList<IBudgetItem> itms = BudgetItem.GetDao().RetrieveOpen(mSecurityContext);

                    mBudgetItems = new List<IBudgetItemViewModel>();
                    foreach(IBudgetItem itm in itms) {
                        mBudgetItems.Add(itm.View(mSecurityContext));
                    }
                }
                return mBudgetItems;
            }
        }

        public IBudgetItemViewModel SelectedItem { get; set; }
    }
}
