using dougnlamb.budget;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.budget.budget {
    [TestClass]
    public class AccountTest {
        [TestMethod]
        public void CreateAccountTest() {
            IUser usr = new User();
            IAccountEditorModel model = usr.CreateAccount(null);
            model.Name = "Bubba";
            IAccount account = model.Save(null);

            Assert.AreEqual(model.Name, account.Name);
        }


        [TestMethod]
        public void RetrieveAccountTest() {
            IAccount account = Account.GetDao().Retrieve(null, 1000);

            Assert.AreEqual(1000, account.oid);
            Assert.AreEqual("Bubba's account", account.Name);
        }

        [TestMethod]
        public void LazyLoadAccountTest() {
            IAccount account = new Account(1000);

            Assert.AreEqual(1000, account.oid);
            Assert.AreEqual("Bubba's account", account.Name);
        }
    }
}
