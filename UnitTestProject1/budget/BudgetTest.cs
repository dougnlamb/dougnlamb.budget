﻿using dougnlamb.budget;
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
    public class BudgetTest {
        [TestInitialize]
        public void init() {
            MockDatabase.init();
        }

        [TestMethod]
        public void CreateBudgetTest() {
            IUser usr = User.GetDao().Retrieve(null, 1);
            IBudgetEditorModel model = usr.CreateBudget(null);
            model.DefaultCurrency = Currency.GetDao().Retrieve(null, 1);
            model.Name = "Bubba";
            IBudget budget = model.Save(null);

            Assert.AreEqual(model.Name, budget.Name);
            Assert.IsTrue(budget.oid > 0);

            IBudget b = Budget.GetDao().Retrieve(null,budget.oid);

            Assert.AreEqual(budget.oid, b.oid);
            Assert.AreEqual(budget.Name, b.Name);
            Assert.AreEqual(usr.oid, budget.Owner.oid);
        }


        [TestMethod]
        public void RetrieveBudgetTest() {
            IBudget budget = Budget.GetDao().Retrieve(null, 1);

            Assert.AreEqual(1, budget.oid);
            Assert.AreEqual("Bubba", budget.Name);
        }

        [TestMethod]
        public void LazyLoadBudgetTest() {
            IBudget budget = new Budget(null, 1);

            Assert.AreEqual(1, budget.oid);
            Assert.AreEqual("Bubba", budget.Name);
        }
    }
}
