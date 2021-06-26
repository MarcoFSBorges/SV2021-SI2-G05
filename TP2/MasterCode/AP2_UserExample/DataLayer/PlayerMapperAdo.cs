using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PlayerMapperAdo : AbstractMapper<Player, int?, List<Player>>, IPlayerMapper
    {
        public PlayerMapperAdo(Context ctx) : base(ctx)
        { 
        }

        public Player Create(Player player)
        {
            ensureContext();

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
            ensureContext();

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

        public Player Delete(Player player)
        {
            ensureContext();

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
            ensureContext();
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

        public Player Read(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Player> ReadAll()
        {
            ensureContext();
            List<Player> players = new List<Player>();

            using (SqlCommand cmd = context.con.CreateCommand())
            {
                cmd.Transaction = context.tran;
                cmd.CommandText = "SELECT player_id, username, deleted FROM Player;";


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Player player = new Player();

                        player.Username = (string)dr["username"];
                        player.PlayerID = (int)dr["player_id"];
                        player.Deleted = (bool)dr["deleted"];

                        players.Add(player);
                    }
                }
            }
            return players;
        }

        public Login Update(Login login)
        {
            ensureContext();
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
    }
}
