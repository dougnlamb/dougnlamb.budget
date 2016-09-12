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
    public class AllocationTest {
        [TestInitialize]
        public void init() {
            MockDatabase.init();
        }

        [TestMethod]
        public void CreateAllocationTest() {
            IBudgetItem budgetItem = BudgetItem.GetDao().Retrieve(null, 1000);
            decimal balance = budgetItem.Amount.Amount - 25;

            ITransaction transaction = Transaction.GetDao().Retrieve(null, 1000);
            IAllocationEditorModel model = transaction.CreateAllocation(null);
            model.BudgetItem = budgetItem;
            model.Notes = "Just a plain allocation";
            model.Amount = new Money() { Amount = -25.25M, Currency = model.BudgetItem.Amount.Currency };

            IAllocation allocation = model.Save(null);
            Assert.AreEqual(model.Notes, allocation.Notes);
            Assert.AreEqual(model.Amount.Amount, allocation.Amount.Amount);
            Assert.AreEqual(model.Amount.Currency.Code, allocation.Amount.Currency.Code);
            Assert.AreEqual(balance - 25.25M, allocation.BudgetItem.Balance.Amount);

            Assert.IsTrue(allocation.oid > 0);
            IAllocation a = Allocation.GetDao().Retrieve(null, allocation.oid);

            Assert.AreEqual(a.Notes, allocation.Notes);
            Assert.AreEqual(a.Amount.Amount, allocation.Amount.Amount);
            Assert.AreEqual(a.Amount.Currency.Code, allocation.Amount.Currency.Code);
        }


        [TestMethod]
        public void RetrieveAllocationTest() {
            IAllocation allocation = Allocation.GetDao().Retrieve(null, 1000);

            Assert.AreEqual(1000, allocation.oid);
            Assert.AreEqual("My Allocation", allocation.Notes);
            Assert.AreEqual(-25, allocation.Amount.Amount);
        }

        [TestMethod]
        public void LazyLoadAllocationTest() {
            IAllocation allocation = new Allocation(null, 1000);

            Assert.AreEqual(1000, allocation.oid);
            Assert.AreEqual("My Allocation", allocation.Notes);
            Assert.AreEqual(-25, allocation.Amount.Amount);
        }
    }
}
