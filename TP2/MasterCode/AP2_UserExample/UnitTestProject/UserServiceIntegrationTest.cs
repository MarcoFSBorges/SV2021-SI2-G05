using System;
using System.Collections.Generic;
using BusinessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace UnitTestProject
{
    [TestClass]
    public class UserServiceIntegrationTest
    {
        [TestMethod]
        public void TestCreateUser()
        {
            PlayerService userService = new PlayerService();

            string generatedUsername = "test user " + DateTime.Now;
            string generatedName = "test name " + DateTime.Now;

            Player user = new Player()
            {
                Username = generatedUsername,
            };

            user = userService.createPlayer(user);

            Assert.IsNotNull(user, "user must exist");
            Assert.AreEqual(generatedUsername, user.Username);
        }


        [TestMethod]
        public void TestGetAllUsers()
        {
            PlayerService userService = new PlayerService();

            IList<Player> allUsers = userService.getAll();
       
            Assert.IsNotNull(allUsers, "getAll must be not null");
            Assert.AreNotEqual(0, allUsers);

        }
    }

}
