using dougnlamb.budget;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.budget.budget {
    [TestClass]
    public class UserTest {
        [TestMethod]
        public void CreateUserTest() {
            IUser usr = new User();
            IUserEditorModel model = usr.Edit(null);
            model.DisplayName = "Doug";
            model.Email = "dougnlamb@gmail.com";

            usr.Save(usr, model);

            Assert.AreEqual(model.DisplayName, usr.DisplayName);
            Assert.AreEqual(model.Email, usr.Email);

        }

        [TestMethod]
        public void CreateAccountTest() {
            IUser usr = new User();
            IAccountEditorModel model = usr.CreateAccount();
            model.Name = "Bubba";
        }
    }
}
