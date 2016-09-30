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
            IBudgetItem budgetItem = BudgetItem.GetDao().Retrieve(null, 1);
            decimal balance = budgetItem.BudgetAmount.Value;

            ITransaction transaction = Transaction.GetDao().Retrieve(null, 1);
            IAllocationEditorModel model = transaction.CreateAllocation(null);
            model.BudgetItem = budgetItem;
            model.Notes = "Just a plain allocation";
            model.Amount = new Money() { Value = -25.25M, Currency = model.BudgetItem.BudgetAmount.Currency };

            IAllocation allocation = model.Save(null);
            Assert.AreEqual(model.Notes, allocation.Notes);
            Assert.AreEqual(model.Amount.Value, allocation.Amount.Value);
            Assert.AreEqual(model.Amount.Currency.Code, allocation.Amount.Currency.Code);
            Assert.AreEqual(balance - 25.25M, allocation.BudgetItem.Balance.Value);

            Assert.IsTrue(allocation.oid > 0);
            IAllocation retrievedAllocation = Allocation.GetDao().Retrieve(null, allocation.oid);

            Assert.AreEqual(retrievedAllocation.Notes, allocation.Notes);
            Assert.AreEqual(retrievedAllocation.Amount.Value, allocation.Amount.Value);
            Assert.AreEqual(retrievedAllocation.Amount.Currency.Code, allocation.Amount.Currency.Code);
        }


        [TestMethod]
        public void RetrieveAllocationTest() {
            IAllocation allocation = Allocation.GetDao().Retrieve(null, 3);

            Assert.AreEqual(3, allocation.oid);
            Assert.AreEqual("Just a plain allocation", allocation.Notes);
            Assert.AreEqual(-25.25M, allocation.Amount.Value);
        }

        [TestMethod]
        public void LazyLoadAllocationTest() {
            IAllocation allocation = new Allocation(null, 3);

            Assert.AreEqual(3, allocation.oid);
            Assert.AreEqual("Just a plain allocation", allocation.Notes);
            Assert.AreEqual(-25.25M, allocation.Amount.Value);
        }
    }
}
