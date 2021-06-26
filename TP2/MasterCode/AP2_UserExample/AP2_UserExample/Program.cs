using BusinessLayer;
using DataLayer;
using Model;
using System;
using System.Collections.Generic;

namespace AP2_UserExample
{
    class Program
    {
        static void Main()
        {
            PlayerService playerService = new PlayerService(GetSolutionType());

            Run(playerService);

            //testPlayerView(playerService);
        }

        private static SolutionType GetSolutionType()
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

        private static void Run(PlayerService playerService)
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
                        TestGetAllPlayer(playerService);
                        break;
                    case '2':
                        TestCreatePlayer(playerService);
                        break;
                    case '3':
                        TestUpdatePlayer(playerService);
                        break;
                    case '4':
                        TestDeletePlayer(playerService);
                        break;
                    case '5':
                        TestReadClans(playerService);
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
            Console.WriteLine("5\tCheck Clans");
            Console.WriteLine("0\tExit");
        }

        private static void TestReadClans(PlayerService playerService)
        {
            IList<Clan> allClans = playerService.GetClansOrClan("");

            if (allClans.Count == 1)
            {
                Clan c = allClans[0];
                Console.WriteLine("{0}\t{1}\t{2}", c.ClanID, c.ClanName, c.ClanScore);
            }
            else
            {
                foreach (Clan c in allClans)
                {
                    Console.WriteLine(c.ClanName);
                }
            }
        }

        private static void TestGetAllPlayer(PlayerService playerService)
        {
            IList<Player> allUsers = playerService.ReadAll();

            foreach (Player p in allUsers)
            {
                Console.WriteLine(p.Username);
            }
        }

        private static void TestCreatePlayer(PlayerService playerService)
        {

            Player p = new Player
            {
                Username = "DummyFather1"
            };

            playerService.CreatePlayer(p);
        }

        private static void TestUpdatePlayer(PlayerService playerService)
        {

            Player p = new Player
            {
                Username = "testerDummy1"
            };

            Login l = new Login
            {
                UserEmail = "newEmail@hotmail.com",
                Username = p.Username,
                Name = "master dummy",
                Password = "VNl8iccweptzX2r",
                Birthday = "1970-03-29"
            };
            playerService.UpdatePlayer(l);
        }

        private static void TestPlayerView(PlayerService playerService)
        {
            playerService.PlayerView();
        }

        private static void TestDeletePlayer(PlayerService playerService)
        {

            Player p = new Player
            {
                Username = "DummyFather"
            };

            playerService.DeletePlayer(p);
        }
    }
}
