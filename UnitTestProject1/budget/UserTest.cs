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

            model.Save(null);

            Assert.AreEqual(model.DisplayName, usr.DisplayName);
            Assert.AreEqual(model.Email, usr.Email);

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

    }
}
