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
            IUser usr = User.GetDao().Retrieve(null, 1);
            IAccountEditorModel model = usr.CreateAccount(null);
            model.DefaultCurrency = usr.DefaultCurrency;
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
        public void UpdateAccountTest() {
            string name = $"Bubba {DateTime.Now.ToLongTimeString()}";
            IAccount account = Account.GetDao().Retrieve(null, 1);
            IAccountEditorModel model = new AccountEditorModel(null, account);
            model.Name = name;
            model.DefaultCurrency = Currency.GetDao().Retrieve(null, 1);
            model.Save(null);

            Assert.AreEqual(name, account.Name);

            Assert.IsTrue(account.oid == 1);
            IAccount a = Account.GetDao().Retrieve(null, account.oid);

            Assert.AreEqual(account.oid, a.oid);
            Assert.AreEqual(name, a.Name);
            Assert.AreEqual("USD", a.DefaultCurrency.Code);
            Assert.AreEqual(1, account.Owner.oid);
        }


        [TestMethod]
        public void RetrieveAccountTest() {
            IAccount account = Account.GetDao().Retrieve(null, 1);

            Assert.AreEqual(1, account.oid);
            //Assert.AreEqual("Bubba", account.Name);
            Assert.AreEqual(1, account.Owner.oid);
            Assert.AreEqual(1, account.DefaultCurrency.oid);
        }

        [TestMethod]
        public void LazyLoadAccountTest() {
            IAccount account = new Account(null, 1);

            Assert.AreEqual(1, account.oid);
            //Assert.AreEqual("Bubba", account.Name);
            Assert.AreEqual(1, account.Owner.oid);
            Assert.AreEqual(1, account.DefaultCurrency.oid);
        }

        [TestMethod]
        public void CreateTransactionTest() {
            IUser usr = User.GetDao().Retrieve(null, 1);
            Assert.IsNotNull(usr);
            IAccount account = Account.GetDao().Retrieve(null, 1);
            Assert.IsNotNull(account);
            TransactionEditorModel model = new TransactionEditorModel(null, usr, account);

            model.TransactionAmountEditor.Amount = 100;
            model.TransactionAmountEditor.CurrencySelector.SelectedCurrencyCode = account.DefaultCurrency.oid;

            ITransaction transaction = account.AddTransaction(null, model);

            transaction = Transaction.GetDao().Retrieve(null, transaction.oid);
            Assert.AreEqual(model.TransactionAmount.Value, transaction.TransactionAmount.Value);
        }
    }
}
