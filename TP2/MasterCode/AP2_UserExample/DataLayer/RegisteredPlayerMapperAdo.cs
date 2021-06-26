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
    public class RegisteredPlayerMapperAdo : AbstractMapper<RegisteredPlayer, int?, List<RegisteredPlayer>>, IRegisteredPlayerMapper
    {
        public RegisteredPlayerMapperAdo(Context ctx) : base(ctx)
        {
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
    }
}
