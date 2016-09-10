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
    public class AccountTest {
        [TestInitialize]
        public void init() {
            MockDatabase.init();
        }

        [TestMethod]
        public void CreateAccountTest() {
            IUser usr = User.GetDao().Retrieve(null, 1000);
            IAccountEditorModel model = usr.CreateAccount(null);
            ((AccountEditorModel)model).CurrencySelector.SelectedCurrencyId = 1000;
            model.Name = "Bubba";
            IAccount account = model.Save(null);

            Assert.AreEqual(model.Name, account.Name);

            Assert.IsTrue(account.oid > 0);
            IAccount a = Account.GetDao().Retrieve(null, account.oid);

            Assert.AreEqual(account.oid, a.oid);
            Assert.AreEqual(account.Name, a.Name);
            Assert.AreEqual(usr.oid, account.Owner.oid);
        }


        [TestMethod]
        public void RetrieveAccountTest() {
            IAccount account = Account.GetDao().Retrieve(null, 1000);

            Assert.AreEqual(1000, account.oid);
            Assert.AreEqual("Bubba's account", account.Name);
        }

        [TestMethod]
        public void LazyLoadAccountTest() {
            IAccount account = new Account(null, 1000);

            Assert.AreEqual(1000, account.oid);
            Assert.AreEqual("Bubba's account", account.Name);
        }

        [TestMethod]
        public void CreateTransactionTest() {
            IAccount account = Account.GetDao().Retrieve(null, 1000);
            ITransactionEditorModel model = account.CreateTransaction(null);
            model.TransactionAmount.Amount = 100;
            ITransaction transaction = account.AddTransaction(null, model);

            Assert.AreEqual(1002, transaction.oid);
            Assert.AreEqual(100, transaction.TransactionAmount.Amount);
        }
    }
}
