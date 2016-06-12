using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarlockServerDAL.Managers;

namespace Tests.DALs
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void TestUserIntegration()
        {
            using (UsersManager manager = new UsersManager())
            {
                var user = manager.RegisterUser("test", "asd@asd.asd", "test");
                Assert.IsTrue(manager.TryLogin("test", "test", out user));
                manager.DeleteUser(user);
            }
        }
    }
}
