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
            IAccountEditorModel model = account.Edit(null);
            model.Name = name;
            model.DefaultCurrency = new Currency(null, 1000);
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
            Assert.AreEqual("Bubba", account.Name);
            Assert.AreEqual(1, account.Owner.oid);
            Assert.AreEqual(1000, account.DefaultCurrency.oid);
        }

        [TestMethod]
        public void LazyLoadAccountTest() {
            IAccount account = new Account(null, 1);

            Assert.AreEqual(1, account.oid);
            Assert.AreEqual("Bubba", account.Name);
            Assert.AreEqual(1, account.Owner.oid);
            Assert.AreEqual(1000, account.DefaultCurrency.oid);
        }

        [TestMethod]
        public void CreateTransactionTest() {
            IAccount account = Account.GetDao().Retrieve(null, 1000);
            ITransactionEditorModel model = account.CreateTransaction(null);

            model.TransactionAmount = new Money() { Amount = 100, Currency = account.DefaultCurrency };
            ITransaction transaction = account.AddTransaction(null, model);

            Assert.AreEqual(1002, transaction.oid);
            Assert.AreEqual(100, transaction.TransactionAmount.Amount);
        }
    }
}
