using EF;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void GetPlayerView()
        {
            using (EFcontext = new Jogos_entities())
            {
                var playersView = EFcontext.players_view.ToList();
                foreach (var item in playersView)
                {
                    Console.WriteLine(""
                        + item.player_id
                        + " | "
                        + item.login_id
                        + " | "
                        + item.score
                        + " | "
                        + item.level
                        + " | "
                        + item.bank_balance
                        + " | "
                        + item.life_points
                        + " | "
                        + item.strength_points
                        + " | "
                        + item.speed_points
                        + " | "
                        + item.clan_name
                        + " | "
                        + item.item_id
                        + " | "
                        + item.name);
                }
            }
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

        DataTable IPlayerMapper.GetPlayerView()
        {
            throw new NotImplementedException();
        }
    }
}
