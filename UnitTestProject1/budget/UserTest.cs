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
    public class UserTest {
        [TestInitialize]
        public void init() {
            MockDatabase.init();
        }

        [TestMethod]
        public void RegisterUserTest() {
            IUserRegistration registration = new UserRegistration();
            IUserRegistrationModel model = registration.RegisterUser();
            model.UserId = "staceylamb";
            model.DisplayName = "Stacey";
            model.Email = "staceylamb68@gmail.com";
            model.DefaultCurrency = new Currency(null, 1);

            IUser usr = registration.Save(null, model);

            Assert.AreEqual(model.UserId, usr.UserId);
            Assert.AreEqual(model.DisplayName, usr.DisplayName);
            Assert.AreEqual(model.Email, usr.Email);

            Assert.IsTrue(usr.oid > 0);
            IUser u = User.GetDao().Retrieve(null, usr.oid);

            Assert.AreEqual(usr.oid, u.oid);
            Assert.AreEqual(usr.DisplayName, u.DisplayName);
            Assert.AreEqual(usr.Email, u.Email);
        }

        [TestMethod]
        public void RetrieveUserTest() {
            IUser usr = User.GetDao().Retrieve(null, 1);

            Assert.AreEqual(1, usr.oid);
            Assert.AreEqual("dougnlamb", usr.UserId);
            Assert.AreEqual("Doug Lamb", usr.DisplayName);
            Assert.AreEqual("dougnlamb@gmail.com", usr.Email);
        }

        [TestMethod]
        public void LazyLoadUserTest() {
            IUser usr = new User(null, 1);

            Assert.AreEqual(1, usr.oid);
            Assert.AreEqual("dougnlamb", usr.UserId);
            Assert.AreEqual("Doug Lamb", usr.DisplayName);
            Assert.AreEqual("dougnlamb@gmail.com", usr.Email);
        }

        [TestMethod]
        public void AddAccountTest() {
            IUser usr = User.GetDao().Retrieve(null, 1);

            int accountsCount = usr.Accounts.Count;

            IAccountEditorModel editor = usr.CreateAccount(null);
            editor.Name = "Create Account Test";
            editor.DefaultCurrency = usr.DefaultCurrency;
            IAccount acct = usr.AddAccount(null, editor);

            Assert.IsNotNull(acct);

            Assert.AreEqual(accountsCount + 1, usr.Accounts.Count);
            Assert.AreEqual(acct.oid, usr.Accounts[usr.Accounts.Count-1].oid);
            Assert.AreEqual(acct.Name, usr.Accounts[usr.Accounts.Count-1].Name);

            acct = Account.GetDao().Retrieve(null, acct.oid);
            Assert.AreEqual(acct.oid, usr.Accounts[usr.Accounts.Count-1].oid);
            Assert.AreEqual(acct.Name, usr.Accounts[usr.Accounts.Count-1].Name);
        }

        [TestMethod]
        public void AddBudgetTest() {
            IUser usr = User.GetDao().Retrieve(null, 1);
            int budgetsCount = usr.Budgets.Count;
            IBudgetEditorModel editor = usr.CreateBudget(null);
            editor.DefaultCurrency = usr.DefaultCurrency;
            editor.Name = "Create Budget Test";
            IBudget budget = usr.AddBudget(null, editor);

            Assert.IsNotNull(budget);

            Assert.AreEqual(budgetsCount+1, usr.Budgets.Count);
            Assert.AreEqual(budget.oid, usr.Budgets[budgetsCount].oid);
            Assert.AreEqual(budget.Name, usr.Budgets[budgetsCount].Name);

            budget = Budget.GetDao().Retrieve(null, budget.oid);
            Assert.AreEqual(budget.oid, usr.Budgets[budgetsCount].oid);
            Assert.AreEqual(budget.Name, usr.Budgets[budgetsCount].Name);
        }
    }
}
