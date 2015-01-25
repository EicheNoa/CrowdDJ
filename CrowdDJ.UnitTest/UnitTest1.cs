using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrowdDJ.BL;
using CrowdDJ.DomainClasses;

namespace CrowdDJ.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UserTestSuite()
        {
            ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
            User user = new User("Peter von der Alm", "hallo", "peter@hausen.de", false);
            Assert.IsTrue(bl.InsertUser(user));
            Assert.IsFalse(bl.InsertUser(user));
            user = bl.FindUserByEmail(user.Email);
            Assert.IsTrue(bl.DeleteUser(user.UserId));
            user = new User("Heidi von der Wiese", "yoyo", "almdirndl@alm.at", true);
            Assert.IsTrue(bl.InsertUser(user));
            user = bl.FindUserByEmail(user.Email);
            user.Email = "almrocker@alm.at";
            Assert.IsTrue(bl.UpdateUser(user, user.UserId));
            user = null;
            user = bl.FindUserByEmail("almrocker@alm.at");
            Assert.IsTrue(user.Email.Equals("almrocker@alm.at"));
            Assert.IsTrue(bl.DeleteUser(user.UserId));
        }
    }
}
