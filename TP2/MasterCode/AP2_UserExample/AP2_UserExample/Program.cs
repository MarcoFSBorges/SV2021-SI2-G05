using BusinessLayer;
using DataLayer;
using Model;
using System;
using System.Collections.Generic;

namespace AP2_UserExample
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerService playerService = new PlayerService(getSolutionType());

            run(playerService);

            //testPlayerView(playerService);
        }

        private static SolutionType getSolutionType()
        {
            SolutionType res = new SolutionType();
            Console.WriteLine("Escolha a Solução:\n1\tADO\n2\tEF\nAny key\tExit");
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            if (keyPressed.KeyChar == '1')
            {
                res = SolutionType.ADO;
            } else if (keyPressed.KeyChar == '2')
            {
                res = SolutionType.EF;
            } else
            {
                Environment.Exit(0);
            }
            return res;
        }

        private static void run(PlayerService playerService)
        {
            bool exit = false;
            while(!exit)
            {
                Console.Clear();
                PrintOptions();
                ConsoleKeyInfo keyPressed = Console.ReadKey();
                Console.Clear();
                switch (keyPressed.KeyChar)
                {
                    case '0':
                        exit = true;
                        break;
                    case '1':
                        testGetAllPlayer(playerService);
                        break;
                    case '2':
                        testCreatePlayer(playerService);
                        break;
                    case '3':
                        testUpdatePlayer(playerService);
                        break;
                    case '4':
                        testDeletePlayer(playerService);
                        break;
                }
                Console.WriteLine("\nPress Enter to continue. Esc to exit.");
                while(keyPressed.Key != ConsoleKey.Enter && !exit)
                {
                    keyPressed = Console.ReadKey();
                    if (keyPressed.Key == ConsoleKey.Escape)
                    {
                        exit = true;
                    }
                }
            }
        }

        private static void PrintOptions()
        {
            Console.WriteLine("Available operations:");
            Console.WriteLine("1\tGet all players");
            Console.WriteLine("2\tCreate new player");
            Console.WriteLine("3\tUpdate existing player");
            Console.WriteLine("4\tDelete existing player");
            Console.WriteLine("0\tExit");
        }

        private static void testGetAllPlayer(PlayerService playerService)
        {
            IList<Player> allUsers = playerService.readAll();

            foreach (Player p in allUsers)
            {
                Console.WriteLine(p.Username);
            }
        }

        private static void testCreatePlayer(PlayerService playerService)
        {

            Player p = new Player();
            p.Username = "DummyFather1";

            playerService.CreatePlayer(p);
        }

        private static void testUpdatePlayer(PlayerService playerService)
        {

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

        private static void testPlayerView(PlayerService playerService)
        {
            playerService.PlayerView();
        }

        private static void testDeletePlayer(PlayerService playerService)
        {

            Player p = new Player();
            p.Username = "DummyFather";

            playerService.DeletePlayer(p);
        }
    }
}
