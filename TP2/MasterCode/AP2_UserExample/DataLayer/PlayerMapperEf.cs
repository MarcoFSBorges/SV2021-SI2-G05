using EF;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataLayer
{
    class PlayerMapperEf : IPlayerMapper
    {

        protected Jogos_entities EFcontext;
        public readonly Context context;
        public PlayerMapperEf(Context ctx)
        {
            context = ctx;
        }

        public Player Create(Player p)
        {
            /*
             * TODO: analisar novamente o tipo de retorno do método, se faz sentido
             */
            using (EFcontext = new Jogos_entities())
            {
                EFcontext.PLAYER.Add(new PLAYER
                {
                    username = p.Username,
                    deleted = false
                });
                EFcontext.SaveChanges();
            }
            return p;
        }

        public Player Delete(Player p)
        {
            using (EFcontext = new Jogos_entities())
            {

                var playerToRemove = EFcontext.PLAYER.SingleOrDefault(x => x.username == p.Username);
                if (playerToRemove != null)
                {
                    EFcontext.PLAYER.Remove(playerToRemove);
                    EFcontext.SaveChanges();
                }
            }
            return p;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Player Read(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Player> ReadAll()
        {
            List<Player> players = new List<Player>();
            using (EFcontext = new Jogos_entities())
            {
                foreach (var p in EFcontext.PLAYER)
                {
                    players.Add(new Player
                    {
                        PlayerID = p.player_id,
                        Username = p.username,
                        Deleted = (bool)p.deleted
                    });
                }
            }
            return players;
        }

        public Login Update(Login login)
        {
            using (EFcontext = new Jogos_entities())
            {
                LOGIN loginRes = EFcontext.LOGIN.SingleOrDefault(b => b.username == login.Username);

                if (loginRes != null)
                {
                    loginRes.user_email = login.UserEmail;
                    loginRes.name = login.Name;
                    loginRes.password = login.Password;
                    loginRes.birthday = DateTime.Parse(login.Birthday);
                    EFcontext.SaveChanges();
                }
            }
            return login;
        }

        public Player Update(Player entity)
        {
            throw new NotImplementedException();
        }

        public IList<Clan> GetClansOrClan(string clanName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetPlayerView()
        {
            using (EFcontext = new Jogos_entities())
            {
                DataTable dt = new DataTable();
                
                var playersView = EFcontext.players_view.ToList();

                List<DataColumn> cols = new List<DataColumn>();

                cols.Add(new DataColumn
                {
                    ColumnName = "player_id"
                });
                cols.Add(new DataColumn
                {
                    ColumnName = "login_id"
                });
                cols.Add(new DataColumn
                {
                    ColumnName = "score"
                });
                cols.Add(new DataColumn
                {
                    ColumnName = "level"
                });
                cols.Add(new DataColumn
                {
                    ColumnName = "bank_balance"
                });
                cols.Add(new DataColumn
                {
                    ColumnName = "life_points"
                });
                cols.Add(new DataColumn
                {
                    ColumnName = "strength_points"
                });
                cols.Add(new DataColumn
                {
                    ColumnName = "speed_points"
                });
                cols.Add(new DataColumn
                {
                    ColumnName = "clan_name"
                });
                cols.Add(new DataColumn
                {
                    ColumnName = "item_id"
                });
                cols.Add(new DataColumn
                {
                    ColumnName = "name"
                });

                dt.Columns.AddRange(cols.ToArray());

                foreach (var entry in playersView)
                {
                    DataRow row = dt.NewRow();

                    row["player_id"] = entry.player_id;
                    row["login_id"] = entry.login_id;
                    row["score"] = entry.score;
                    row["level"] = entry.level;
                    row["bank_balance"] = entry.bank_balance;
                    row["life_points"] = entry.life_points;
                    row["strength_points"] = entry.strength_points;
                    row["speed_points"] = entry.speed_points;
                    row["clan_name"] = entry.clan_name;
                    row["item_id"] = entry.item_id;
                    row["name"] = entry.name;

                    dt.Rows.Add(row);
                }
                return dt;
            }
        }

        public void OptimisticLocking(Login l)
        {
            using (EFcontext = new Jogos_entities())
            {
                LOGIN loginRes = EFcontext.LOGIN.SingleOrDefault(b => b.username == l.Username);
                if (loginRes != null)
                {
                    loginRes.user_email = l.UserEmail;
                    loginRes.name = l.Name;
                    loginRes.password = l.Password;
                    loginRes.birthday = DateTime.Parse(l.Birthday);
                }

                //faz o update diretamente na base de dados
                EFcontext.Database.ExecuteSqlCommand("UPDATE login SET name = 'Big wolf' WHERE username = 'Muchacha'");

                var saved = false;
                while (!saved)
                {
                    try
                    {
                        //tentativa para guardar alterações na base de dados
                        EFcontext.SaveChanges();
                        saved = true;
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException e)
                    {
                        e.Entries.Single().Reload();
                        throw new NotSupportedException(
                                    "OTIMISTIC LOCKING ERROR: " + e);
                    }
                }
            }
        }

        public RegisteredPlayer CreateWithOptions(RegisteredPlayer player, Item item, Clan clan)
        {
            throw new NotImplementedException();
        }

        public void CreateWithOptions(Login login, Item item, Clan clan)
        {
            throw new NotImplementedException();
        }
    }
}
