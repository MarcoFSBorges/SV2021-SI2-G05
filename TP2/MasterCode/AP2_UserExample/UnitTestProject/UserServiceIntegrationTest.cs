using System;
using System.Collections.Generic;
using BusinessLayer;
using DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace UnitTestProject
{
    [TestClass]
    public class UserServiceIntegrationTest
    {
        private static SolutionType GetSolutionType()
        {
            SolutionType res = new SolutionType();
            Console.WriteLine("Escolha a Solução:\n1\tADO\n2\tEF\nAny key\tExit");
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.KeyChar == '1')
            {
                res = SolutionType.ADO;
            }
            else if (keyPressed.KeyChar == '2')
            {
                res = SolutionType.EF;
            }
            else
            {
                Environment.Exit(0);
            }
            return res;
        }


        [TestMethod]
        public void TestCreateUser()
        {
            PlayerService userService = new PlayerService(GetSolutionType());

            string generatedUsername = "test user " + DateTime.Now;
            string generatedName = "test name " + DateTime.Now;

            Player user = new Player()
            {
                Username = generatedUsername,
            };

            user = userService.CreatePlayer(user);

            Assert.IsNotNull(user, "user must exist");
            Assert.AreEqual(generatedUsername, user.Username);
        }


        [TestMethod]
        public void TestGetAllUsers()
        {
            PlayerService userService = new PlayerService(GetSolutionType());

            IList<Player> allUsers = userService.ReadAll();
       
            Assert.IsNotNull(allUsers, "getAll must be not null");
            Assert.AreNotEqual(0, allUsers);

        }
    }

}
