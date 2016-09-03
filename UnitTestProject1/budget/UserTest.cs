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
            model.UserId = "dougnlamb";
            model.DisplayName = "Doug";
            model.Email = "dougnlamb@gmail.com";

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
            IUser usr = User.GetDao().Retrieve(null, 1000);

            Assert.AreEqual(1000, usr.oid);
            Assert.AreEqual("bubba", usr.UserId);
            Assert.AreEqual("Bubba Gump", usr.DisplayName);
            Assert.AreEqual("bubba@example.com", usr.Email);
        }

        [TestMethod]
        public void LazyLoadUserTest() {
            IUser usr = new User(1000);

            Assert.AreEqual(1000, usr.oid);
            Assert.AreEqual("bubba", usr.UserId);
            Assert.AreEqual("Bubba Gump", usr.DisplayName);
            Assert.AreEqual("bubba@example.com", usr.Email);
        }

        [TestMethod]
        public void AddAccountTest() {
            IUser usr = User.GetDao().Retrieve(null, 1000);
            IAccountEditorModel editor = usr.CreateAccount(null);
            editor.Name = "Create Account Test";
            ((AccountEditorModel)editor).CurrencySelector.SelectedCurrencyId = 1000;
            IAccount acct = usr.AddAccount(null, editor);

            Assert.IsNotNull(acct);

            Assert.AreEqual(1, usr.Accounts.Count);
            Assert.AreEqual(acct.oid, usr.Accounts[0].oid);
            Assert.AreEqual(acct.Name, usr.Accounts[0].Name);

            acct = Account.GetDao().Retrieve(null, 1002);
            Assert.AreEqual(acct.oid, usr.Accounts[0].oid);
            Assert.AreEqual(acct.Name, usr.Accounts[0].Name);
        }

        [TestMethod]
        public void AddBudgetTest() {
            IUser usr = User.GetDao().Retrieve(null, 1000);
            IBudgetEditorModel editor = usr.CreateBudget(null);
            ((BudgetEditorModel)editor).CurrencySelector.SelectedCurrencyId = 1000;
            editor.Name = "Create Budget Test";
            IBudget budget = usr.AddBudget(null, editor);

            Assert.IsNotNull(budget);

            Assert.AreEqual(1, usr.Budgets.Count);
            Assert.AreEqual(budget.oid, usr.Budgets[0].oid);
            Assert.AreEqual(budget.Name, usr.Budgets[0].Name);

            budget = Budget.GetDao().Retrieve(null, 1003);
            Assert.AreEqual(budget.oid, usr.Budgets[0].oid);
            Assert.AreEqual(budget.Name, usr.Budgets[0].Name);
        }
    }
}
