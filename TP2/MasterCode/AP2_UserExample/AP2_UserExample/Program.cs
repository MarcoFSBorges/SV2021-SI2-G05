using BusinessLayer;
using DataLayer;
using Model;
using System;
using System.Data;
using System.Collections.Generic;
using ConsoleTables;

namespace AP2_UserExample
{
    class Program
    {
        static void Main()
        {
            while (true) {
                PlayerService playerService = new PlayerService(GetSolutionType());
                Run(playerService);
            }
        }

        private static SolutionType GetSolutionType()
        {
            Console.Clear();
            SolutionType res = new SolutionType();
            Console.WriteLine("Escolha a Solução:\n1\tADO\n2\tEF\nAny key\tExit");
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            switch (keyPressed.KeyChar)
            {
                case '1':
                    res = SolutionType.ADO;
                    break;
                case '2':
                    res = SolutionType.EF;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Shutting down...");
                    Environment.Exit(0);
                    break;
            }
            return res;
        }

        private static void Run(PlayerService playerService)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                PrintOptions();
                ConsoleKeyInfo keyPressed = Console.ReadKey();
                switch (keyPressed.KeyChar)
                {
                    case '0':
                        exit = true;
                        break;
                    case '1':
                        ReadAllPlayer(playerService);
                        break;
                    case '2':
                        CreatePlayer(playerService);
                        break;
                    case '3':
                        UpdatePlayer(playerService);
                        break;
                    case '4':
                        DeletePlayer(playerService);
                        break;
                    case '5':
                        ReadClans(playerService);
                        break;
                    case '6':
                        GetPlayerView(playerService);
                        break;
                    case '7':
                        TestOtimisticConcurrency(playerService);
                        break;
                }
                Console.WriteLine("\nPress Enter to continue. Esc to exit.");
                while (keyPressed.Key != ConsoleKey.Enter && !exit)
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
            Console.WriteLine("6\tPlayers View");
            Console.WriteLine("7\tOtimistic concurrency test - EF");
            Console.WriteLine("0\tExit");
        }

        private static void ReadClans(PlayerService playerService)
        {
            Console.Clear();
            Console.WriteLine("Insert Clan name for it's details or press Enter to list all Clan names:");

            string input = Console.ReadLine();

            IList<Clan> allClans = playerService.GetClansOrClan(input);

            if (allClans.Count == 1)
            {
                Clan c = allClans[0];
                Console.WriteLine("{0}\t{1}\t{2}", c.ClanID, c.ClanName, c.ClanScore);
            } else if (allClans.Count == 0)
            {
                Console.WriteLine("There's no clan with that name.");
            } else
            {
                foreach (Clan c in allClans)
                {
                    Console.WriteLine(c.ClanName);
                }
            }
        }

        private static void ReadAllPlayer(PlayerService playerService)
        {
            Console.Clear();
            IList<Player> allUsers = playerService.ReadAll();

            foreach (Player p in allUsers)
            {
                Console.WriteLine(p.Username);
            }
        }

        private static void CreatePlayer(PlayerService playerService)
        {
            Console.Clear();

            Console.WriteLine("Pick username:");

            string input = Console.ReadLine();

            Player p = new Player
            {
                Username = input
            };

            Player res = playerService.CreatePlayer(p);

            if (res == null)
            {
                Console.WriteLine("Try again? (y/n)");
                ConsoleKeyInfo keyPressed = Console.ReadKey();
                Console.Clear();
                switch (keyPressed.KeyChar)
                {
                    case 'y':
                        CreatePlayer(playerService);
                        break;
                    case 'n':
                        break;
                }
            }
        }

        private static void UpdatePlayer(PlayerService playerService)
        {
            Console.Clear();
            Console.WriteLine("To update a player insert the following fields:");

            Console.WriteLine("Email:");
            string email = Console.ReadLine();

            Console.WriteLine("Username:");
            string username = Console.ReadLine();

            Console.WriteLine("Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Password:");
            string password = Console.ReadLine();

            Console.WriteLine("Birthday(YYYY-MM-DD):");
            string birthday = Console.ReadLine();

            Login l = new Login
            {
                UserEmail = email,
                Username = username,
                Name = name,
                Password = password,
                Birthday = birthday
            };
            playerService.UpdatePlayer(l);
        }

        private static void GetPlayerView(PlayerService playerService)
        {
            Console.Clear();

            DataTable dt = playerService.PlayerView();

            int tableWidth = GetTableWidth(dt);

            var table = new ConsoleTable();

            List<string> names = new List<string>();

            foreach (DataColumn column in dt.Columns)
            {
                names.Add(column.ColumnName);
            }

            table.AddColumn(names);

            foreach (DataRow row in dt.Rows)
            {
                object[] entries = new object[tableWidth];

                object[] rowItems = row.ItemArray;

                for (int i = 0; i < tableWidth; i++)
                {
                    try
                    {
                        entries[i] = rowItems[i];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        entries[i] = "";
                    }
                }
                table.AddRow(entries);
            }

            table.Write();
        }

        private static int GetTableWidth(DataTable dt)
        {
            int max = 0;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow.ItemArray.Length > max)
                {
                    max = dataRow.ItemArray.Length;
                }
            }
            return max;
        }

        private static void DeletePlayer(PlayerService playerService)
        {
            Console.Clear();

            Console.WriteLine("Insert username of player to delete:");

            string input = Console.ReadLine();

            Player p = new Player
            {
                Username = input
            };

            playerService.DeletePlayer(p);
        }

        private static void TestOtimisticConcurrency(PlayerService playerService)
        {
            Login l = new Login
            {
                UserEmail = "newEmail@hotmail.com",
                Username = "Muchacha",
                Name = "Muchachinha",
                Password = "VNl8iccweptzX2r",
                Birthday = "1970-03-29"
            };
            playerService.OptimisticLocking(l);
        }
    }
}
