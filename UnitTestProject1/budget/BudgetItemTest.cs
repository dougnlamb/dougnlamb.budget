using dougnlamb.budget;
using dougnlamb.budget.dao;
using dougnlamb.budget.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.budget.budget {
    [TestClass]
    public class BudgetItemTest {
        [TestInitialize]
        public void init() {
            MockDatabase.init();
        }

        [TestMethod]
        public void CreateBudgetItemTest() {
            IBudget budget = Budget.GetDao().Retrieve(null, 1);
            int numItems = budget.BudgetItems.Count;

            IBudgetItemEditorModel model = new BudgetItemEditorModel(null, budget.Owner, budget);

            model.Name = "Movies";
            model.Notes = "Raiders of the Lost Ark";

            model.Amount = new Money() { Value = 100, Currency = Currency.GetDao().Retrieve(null, 1) };

            model.DueDate = DateTime.Now.AddMonths(1);
            model.ReminderDate = DateTime.Now.AddMonths(1).AddDays(-7);

            model.Budget = budget;

            IBudgetItem budgetItem = budget.AddBudgetItem(null, model);
            
            Assert.IsNotNull(budgetItem);

            budget = Budget.GetDao().Retrieve(null, 1);
            Assert.AreEqual(numItems + 1, budget.BudgetItems.Count);

        }


        [TestMethod]
        public void RetrieveBudgetItemTest() {
            IBudgetItem budgetItem = BudgetItem.GetDao().Retrieve(null, 1);

            Assert.AreEqual(1, budgetItem.oid);
            Assert.AreEqual("Movies", budgetItem.Name);
        }

        [TestMethod]
        public void LazyLoadBudgetItemTest() {
            IBudgetItem budgetItem = new BudgetItem(null, 1);

            Assert.AreEqual(1, budgetItem.oid);
            Assert.AreEqual("Movies", budgetItem.Name);
        }
    }
}
