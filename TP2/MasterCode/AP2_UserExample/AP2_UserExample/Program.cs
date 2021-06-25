using BusinessLayer;
using DataLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP2_UserExample
{
    class Program
    {
        static void Main(string[] args)
        {

            //testGetAllPlayer();

            //testCreatePlayer();

            //testGetAllPlayer();

            //testUpdatePlayer();

            testPlayerView();

            //testDeletePlayer();

            Console.WriteLine("Fim...");
            Console.ReadKey();
        }

        private static SolutionType getSolutionType()
        {
            Console.WriteLine("Escolha a Solução (ADO, EF):");
            String line = Console.ReadLine();
           return (SolutionType) Enum.Parse(typeof(SolutionType),line);
        }

        private static void testGetAllPlayer()
        {
            PlayerService playerService = new PlayerService(getSolutionType());
            IList<Player> allUsers = playerService.readAll();

            foreach (Player p in allUsers)
            {
                Console.WriteLine(p.Username);
            }
        }

        private static void testCreatePlayer()
        {
            PlayerService playerService = new PlayerService(getSolutionType());

            Player p = new Player();
            p.Username = "DummyFather1";

            playerService.CreatePlayer(p);
        }

        private static void testUpdatePlayer()
        {
            PlayerService playerService = new PlayerService(getSolutionType());

            Player p = new Player();
            p.Username = "testerDummy1";

            Login l = new Login();
            l.UserEmail = "newEmail@hotmail.com";
            l.Username = p.Username;
            l.Name = "master dummy";
            l.Password = "VNl8iccweptzX2r";
            l.Birthday = "1970-03-29";
            playerService.UpdatePlayer(l);
        }

        private static void testPlayerView()
        {
            PlayerService playerService = new PlayerService(getSolutionType());
            playerService.PlayerView();
        }

        private static void testDeletePlayer()
        {
            PlayerService playerService = new PlayerService(getSolutionType());

            Player p = new Player();
            p.Username = "DummyFather";

            playerService.DeletePlayer(p);
        }
    }
}
