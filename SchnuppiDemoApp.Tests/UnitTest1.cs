using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchnuppiDemoApp.Managers;
using System;
using System.Collections.Generic;

namespace SchnuppiDemoApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Login()
        {
            string userName = "admin";
            string password = "test";

            var usersManager = new UsersManager();

            // Login should work
            if (usersManager.TryLogin(userName, password) == null)
            {
                throw new Exception("Login failed");
            }

            // manipulate login
            password = "test1";

            // Login should fail
            if (usersManager.TryLogin(userName, password) is Models.User)
            {
                throw new Exception("Login should have failed");
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var usersManager = new UsersManager();

            List<Models.User> users = usersManager.GetUsers();

            if(users.Count == 0)
            {
                throw new Exception("No users found");
            }
        }
    }
}
