using dougnlamb.budget;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.budget.budget {
    [TestClass]
    public class BudgetTest {
        [TestMethod]
        public void CreateBudgetTest() {
            IUser usr = new User();
            IBudgetEditorModel model = usr.CreateBudget(null);
            model.Name = "Bubba";
            IBudget budget = model.Save(null);

            Assert.AreEqual(model.Name, budget.Name);
            Assert.IsTrue(budget.oid > 0);

            IBudget b = Budget.GetDao().Retrieve(null,budget.oid);

            Assert.AreEqual(budget.oid, b.oid);
            Assert.AreEqual(budget.Name, b.Name);
        }


        [TestMethod]
        public void RetrieveBudgetTest() {
            IBudget budget = Budget.GetDao().Retrieve(null, 1000);

            Assert.AreEqual(1000, budget.oid);
            Assert.AreEqual("Bubba's budget", budget.Name);
        }

        [TestMethod]
        public void LazyLoadBudgetTest() {
            IBudget budget = new Budget(1000);

            Assert.AreEqual(1000, budget.oid);
            Assert.AreEqual("Bubba's budget", budget.Name);
        }
    }
}
