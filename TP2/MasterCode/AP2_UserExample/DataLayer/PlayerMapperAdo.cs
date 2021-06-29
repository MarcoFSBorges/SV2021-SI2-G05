using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DataLayer
{
    public class PlayerMapperAdo : IPlayerMapper
    {
        public readonly Context context;
        public PlayerMapperAdo(Context ctx) 
        {
            context = ctx;
        }

        public void EnsureContext()
        {
            if (context == null)
            {
                throw new Exception("A contexto não foi bem inicializada...");
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Player Create(Player player)
        {
            EnsureContext();

            using (SqlCommand cmd = context.con.CreateCommand())
            {
                cmd.Transaction = context.tran;
                cmd.CommandText = "INSERT into Player(username) VALUES (@username)";
                cmd.CommandType = CommandType.Text; //default

                SqlParameter playerNameParameter = new SqlParameter("@username", player.Username);
                cmd.Parameters.Add(playerNameParameter);

                int updated = cmd.ExecuteNonQuery();
                context.Commit();

                Console.WriteLine("created {0} registry.", updated);
            }
            return player;
        }

        public void CreateWithOptions(Login login, Item item, Clan clan)
        {
            EnsureContext();

            using (SqlCommand cmd = context.con.CreateCommand())
            {
                cmd.Transaction = context.tran;
                cmd.CommandText = "registerPlayerWithOptions";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter userEmailParameter = new SqlParameter("@user_email", login.UserEmail);
                cmd.Parameters.Add(userEmailParameter);

                SqlParameter usernameParameter = new SqlParameter("@username", login.Username);
                cmd.Parameters.Add(usernameParameter);

                SqlParameter nameParameter = new SqlParameter("@name", login.Name);
                cmd.Parameters.Add(nameParameter);

                SqlParameter passwordParameter = new SqlParameter("@password", login.Password);
                cmd.Parameters.Add(passwordParameter);

                SqlParameter birthdayParameter = new SqlParameter("@birthday", login.Birthday);
                cmd.Parameters.Add(birthdayParameter);

                SqlParameter itemIDParameter;
                if (item == null)
                {
                    itemIDParameter = new SqlParameter("@item_id", item);
                } else
                {
                    itemIDParameter = new SqlParameter("@item_id", item.ItemID);
                }
                cmd.Parameters.Add(itemIDParameter);

                SqlParameter clanIDParameter;
                if (clan == null)
                {
                    clanIDParameter = new SqlParameter("@clan_name", clan);
                }
                else
                {
                    clanIDParameter = new SqlParameter("@clan_name", clan.ClanName);
                }
                cmd.Parameters.Add(clanIDParameter);

                int updated = cmd.ExecuteNonQuery();
                context.Commit();

                Console.WriteLine("created {0} registry.", updated);
            }
        }

        public Player Delete(Player player)
        {
            EnsureContext();

            using (SqlCommand cmd = context.con.CreateCommand())
            {
                cmd.Transaction = context.tran;
                cmd.CommandText = "DELETE FROM Login WHERE username = @username;";

                cmd.CommandType = CommandType.Text; //default

                SqlParameter playerNameParameter = new SqlParameter("@username", player.Username);
                cmd.Parameters.Add(playerNameParameter);

                int deleted = cmd.ExecuteNonQuery();


                context.Commit();

                Console.WriteLine("deleted {0} registry.", deleted);
            }
            return player;
        }

        public DataTable GetPlayerView()
        {
            EnsureContext();
            using (SqlCommand cmd = context.con.CreateCommand())
            {
                cmd.Transaction = context.tran;
                cmd.CommandText = "SELECT player_id, login_id, score, level, bank_balance, life_points, strength_points, speed_points, clan_name, item_id, name FROM players_view";

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public Player Read(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Player> ReadAll()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            EnsureContext();
            List<Player> players = new List<Player>();

            using (SqlCommand cmd = context.con.CreateCommand())
            {
                cmd.Transaction = context.tran;
                cmd.CommandText = "SELECT player_id, username, deleted FROM Player WHERE deleted=0;";


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Player player = new Player
                        {
                            Username = (string)dr["username"],
                            PlayerID = (int)dr["player_id"],
                            Deleted = (bool)dr["deleted"]
                        };

                        players.Add(player);
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
            return players;
        }

        public Login Update(Login login)
        {
            EnsureContext();
            using (SqlCommand cmd = context.con.CreateCommand())
            {
                cmd.Transaction = context.tran;
                cmd.CommandText = "UPDATE Login SET user_email = @user_email, name = @name, password = @password, birthday = @birthday WHERE username = @username; ";

                cmd.CommandType = CommandType.Text;

                SqlParameter EmailParameter = new SqlParameter("@user_email", login.UserEmail);
                SqlParameter NameParameter = new SqlParameter("@name", login.Name);
                SqlParameter PasswordParameter = new SqlParameter("@password", login.Password);
                SqlParameter BirthdayParameter = new SqlParameter("@birthday", login.Birthday);
                SqlParameter UsernameParameter = new SqlParameter("@username", login.Username);
                cmd.Parameters.Add(EmailParameter);
                cmd.Parameters.Add(NameParameter);
                cmd.Parameters.Add(PasswordParameter);
                cmd.Parameters.Add(BirthdayParameter);
                cmd.Parameters.Add(UsernameParameter);

                int updated = cmd.ExecuteNonQuery();
                context.Commit();

                Console.WriteLine("updated {0} registry.", updated);

                //neste caso o que faz sentido retornar,
                //é necessário alterar o modelo de column para conter login no ado.net ?
                return login;
            }
        }

        public IList<Clan> GetClansOrClan(string clanName)
        {
            EnsureContext();

            IList<Clan> result = new List<Clan>();

            using (SqlCommand cmd = context.con.CreateCommand())
            {
                cmd.Transaction = context.tran;
                bool singularMode = false;
                //equivalent of clanName being null
                if (clanName.Equals(""))
                {
                    cmd.CommandText = "SELECT clan_name FROM Clan;";
                    cmd.CommandType = CommandType.Text;
                }
                else
                {
                    singularMode = true;
                    cmd.CommandText = "SELECT clan_id, clan_name, clan_score FROM Clan WHERE clan_name = @clan_name;";
                    cmd.CommandType = CommandType.Text;
                    SqlParameter ClanNameParameter = new SqlParameter("@clan_name", clanName);
                    cmd.Parameters.Add(ClanNameParameter);
                }

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (singularMode)
                        {
                            result.Add(
                                new Clan
                                {
                                    ClanID = (int)dr["clan_id"],
                                    ClanName = (string)dr["clan_name"],
                                    ClanScore = (int)dr["clan_score"]
                                }
                            );
                        }
                        else
                        {
                            result.Add(
                                new Clan
                                {
                                    ClanName = (string)dr["clan_name"]
                                }
                            );
                        }
                    }

                }
            }
            return result;
        }

        public Player Update(Player entity)
        {
            throw new NotImplementedException();
        }

        public void OptimisticLocking(Login l)
        {
            throw new NotImplementedException();
        }
    }
}
