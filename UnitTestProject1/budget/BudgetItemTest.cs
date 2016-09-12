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
            IBudget budget = Budget.GetDao().Retrieve(null, 1000);
            IBudgetItemEditorModel model = budget.CreateBudgetItem(null);

            model.Name = "Movies";
            model.Notes = "Raiders of the Lost Ark";

            model.Amount = new Money() { Amount = 100, Currency = Currency.GetDao().Retrieve(null, 1000) };

            model.DueDate = DateTime.Now.AddMonths(1);
            model.ReminderDate = DateTime.Now.AddMonths(1).AddDays(-7);

            IBudgetItem budgetItem = budget.AddBudgetItem(null, model);

        }


        [TestMethod]
        public void RetrieveBudgetItemTest() {
            IBudgetItem budgetItem = BudgetItem.GetDao().Retrieve(null, 1000);

            Assert.AreEqual(1000, budgetItem.oid);
            Assert.AreEqual("Entertainment", budgetItem.Name);
        }

        [TestMethod]
        public void LazyLoadBudgetTest() {
            IBudgetItem budgetItem = new BudgetItem(null, 1000);

            Assert.AreEqual(1000, budgetItem.oid);
            Assert.AreEqual("Entertainment", budgetItem.Name);
        }
    }
}
