using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class PlayerMapperAdo : AbstractMapper<Player, int?, List<Player>>, IPlayerMapper
    {
        public PlayerMapperAdo(Context ctx) : base(ctx)
        { 
        }

        public new Player Create(Player player)
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
                context.commit();

                Console.WriteLine("created {0} registry.", updated);
            }
            return player;
        }

        public RegisteredPlayer CreateWithOptions(RegisteredPlayer player, Item item, Clan clan)
        {
            EnsureContext();

            using (SqlCommand cmd = context.con.CreateCommand())
            {
                cmd.Transaction = context.tran;
                cmd.CommandText = "createPlayerWithOptions";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter playerIDParameter = new SqlParameter("@player_id", player.PlayerID);
                cmd.Parameters.Add(playerIDParameter);

                SqlParameter loginIDParameter = new SqlParameter("@login_id", player.LoginID);
                cmd.Parameters.Add(loginIDParameter);

                SqlParameter lifePointsParameter = new SqlParameter("@life_points", player.LifePoints);
                cmd.Parameters.Add(lifePointsParameter);

                SqlParameter strengthPointsParameter = new SqlParameter("@strength_points", player.StrengthPoints);
                cmd.Parameters.Add(strengthPointsParameter);

                SqlParameter speedPointsParameter = new SqlParameter("@speed_points", player.SpeedPoints);
                cmd.Parameters.Add(speedPointsParameter);

                SqlParameter itemIDParameter = new SqlParameter("@item_id", item.ItemID);
                cmd.Parameters.Add(itemIDParameter);

                SqlParameter clanIDParameter = new SqlParameter("@clan_id", clan.ClanID);
                cmd.Parameters.Add(clanIDParameter);

                int updated = cmd.ExecuteNonQuery();
                context.commit();

                Console.WriteLine("created {0} registry.", updated);
            }
            return player;
        }

        /*public List<Clan> GetClansOrClan(string clanName)
        {
            ensureContext();

            List<Clan> result = new List<Clan>();
            
            using (SqlCommand cmd = context.con.CreateCommand())
            {
                cmd.Transaction = context.tran;
                bool singleMode = false;

                //equivalent of clanName being null
                if (clanName.Equals(""))
                {
                    cmd.CommandText = "SELECT clan_name FROM Clan;";
                    cmd.CommandType = CommandType.Text;
                }
                else
                {
                    singleMode = true;
                    cmd.CommandText = "SELECT clan_id, clan_name, clan_score FROM Clan WHERE clan_name = @clan_name;";
                    cmd.CommandType = CommandType.Text;
                    SqlParameter ClanNameParameter = new SqlParameter("@clan_name", clanName);
                    cmd.Parameters.Add(ClanNameParameter);
                }

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
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
            return result;
        }*/

        public new Player Delete(Player player)
        {
            EnsureContext();

            using (SqlCommand cmd = context.con.CreateCommand())
            {
                cmd.Transaction = context.tran;
                cmd.CommandText = "DELETE FROM Login WHERE username = @username";
                cmd.CommandType = CommandType.Text; //default

                SqlParameter playerNameParameter = new SqlParameter("@username", player.Username);
                cmd.Parameters.Add(playerNameParameter);

                int deleted = cmd.ExecuteNonQuery();
                context.commit();

                Console.WriteLine("deleted {0} registry.", deleted);
            }
            return player;
        }

        public void GetPlayerView()
        {
            EnsureContext();
            using (SqlCommand cmd = context.con.CreateCommand())
            {
                cmd.Transaction = context.tran;
                cmd.CommandText = "SELECT player_id, login_id, score, level, bank_balance, life_points, strength_points, speed_points, clan_name, item_id, name FROM players_view";

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dataRow in dt.Rows)
                {
                    foreach (var column in dataRow.ItemArray)
                    {
                        Console.Write(column);
                        Console.Write("\t");
                    }
                    Console.WriteLine();
                }
            }
        }

        public new Player Read(int? id)
        {
            throw new NotImplementedException();
        }

        public new List<Player> ReadAll()
        {
            EnsureContext();
            List<Player> players = new List<Player>();

            using (SqlCommand cmd = context.con.CreateCommand())
            {
                cmd.Transaction = context.tran;
                cmd.CommandText = "SELECT player_id, username, deleted FROM Player;";


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
                context.commit();

                Console.WriteLine("updated {0} registry.", updated);

                //neste caso o que faz sentido retornar,
                //é necessário alterar o modelo de column para conter login no ado.net ?
                return login;
            }
        }

        IList<Clan> IPlayerMapper.GetClansOrClan(string clanName)
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
    }
}
