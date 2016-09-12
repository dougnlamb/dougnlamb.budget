﻿using System;
using dougnlamb.budget.models;
using dougnlamb.core.security;

namespace dougnlamb.budget {
    internal class BudgetItemEditorModel : IBudgetItemEditorModel {
        private IBudgetItem mBudgetItem;
        private ISecurityContext mSecurityContext;

        public BudgetItemEditorModel(ISecurityContext securityContext, IBudgetItem budgetItem) {
            this.mSecurityContext = securityContext;
            this.mBudgetItem = budgetItem;
            this.oid = budgetItem?.oid ?? 0;
            this.Name = budgetItem?.Name ?? "";
            this.Notes = budgetItem?.Notes ?? "";
            this.AmountEditor = new MoneyEditorModel(budgetItem?.Amount);
            this.IsClosed = budgetItem?.IsClosed ?? false;
            this.ClosedBy = budgetItem?.ClosedBy ?? null;
            this.DueDate = budgetItem?.DueDate ?? DateTime.Now.AddMonths(1);
            this.ReminderDate = budgetItem?.ReminderDate ?? DueDate.AddDays(-7);

            this.DefaultAccountSelector = new AccountSelectionModel(mSecurityContext, budgetItem?.DefaultAccount);
            this.BudgetSelector = new BudgetSelectionModel(mSecurityContext, budgetItem?.Budget);
        }

        public int oid { get; internal set; }
        public IMoney Amount {
            get {
                return new Money() { Amount = AmountEditor.Amount, Currency = AmountEditor.Currency };
            }
            set {
                AmountEditor.Amount = value?.Amount ?? 0;
                AmountEditor.CurrencySelector.SelectedItem = value?.Currency?.View(mSecurityContext) ?? null;
            }
        } 
        public MoneyEditorModel AmountEditor { get; set; }
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsClosed { get; internal set; }
        public IUser ClosedBy { get; internal set; }
        public DateTime ClosedDate { get; internal set; }
        public IMoney Balance { get; internal set; }
        public bool MarkClosed { get; set; }
        public bool UpdateBalance { get; set; }

        public IBudgetSelectionModel BudgetSelector { get; set; }
        public IAccountSelectionModel DefaultAccountSelector { get; set; }

        public IAccount DefaultAccount {
            get {
                return DefaultAccountSelector?.SelectedAccount;
            }
            set {
                DefaultAccountSelector.SelectedItem = value?.View(mSecurityContext) ?? null;
            }
        }

        public IBudget Budget {
            get {
                return BudgetSelector.SelectedBudget;
            }
            set {
                BudgetSelector.SelectedItem = value?.View(mSecurityContext) ?? null;
            }
        }

        private void Close() {
            IsClosed = true;
            //ClosedBy = user;
            ClosedDate = DateTime.Now;
        }

        private void UpdateBudgetItemBalance() {
            IMoney bal = new Money() { Amount = Amount.Amount, Currency = Amount.Currency };

            foreach (IAllocation allocation in mBudgetItem?.Allocations) {
                bal.Add(allocation.Amount);
            }
            Balance = bal;
        }

        public IBudgetItem Save(ISecurityContext securityContext) {
            if (mBudgetItem == null) {
                if (oid == 0) {
                    mBudgetItem = new BudgetItem(securityContext);
                }
                else {
                    mBudgetItem = BudgetItem.GetDao().Retrieve(securityContext, oid);
                }
            }

            if (MarkClosed) {
                Close();
            }

            if (UpdateBalance) {
                UpdateBudgetItemBalance();
            }

            mBudgetItem.Save(securityContext, this);

            return mBudgetItem;
        }
    }
}