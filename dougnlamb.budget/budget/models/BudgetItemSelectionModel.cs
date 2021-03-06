﻿using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.models {
    public class BudgetItemSelectionModel : IBudgetItemSelectionModel {
        private ISecurityContext mSecurityContext;
        private IBudget mBudget;

        public BudgetItemSelectionModel() { }
        public BudgetItemSelectionModel(ISecurityContext securityContext, IBudget budget, IBudgetItem budgetItem) {
            this.mSecurityContext = securityContext;
            this.mBudget = budget;
            this.SelectedItem = budgetItem?.View(securityContext) ?? null;
        }

        private IList<IBudgetItemViewModel> mBudgetItems;
        public IList<IBudgetItemViewModel> BudgetItems {
            get {
                if (mBudgetItems == null) {
                    IList<IBudgetItem> itms = BudgetItem.GetDao().RetrieveOpen(mSecurityContext, mBudget);

                    mBudgetItems = new List<IBudgetItemViewModel>();
                    foreach (IBudgetItem itm in itms) {
                        mBudgetItems.Add(itm.View(mSecurityContext));
                    }
                }
                return mBudgetItems;
            }
        }

        public IBudgetItemViewModel SelectedItem { get; set; }
        public IBudgetItem SelectedBudgetItem {
            get {
                if (SelectedItem == null) {
                    return null;
                }
                else {
                    return new BudgetItem(mSecurityContext, SelectedItem.oid);
                }
            }
        }
    }
}
