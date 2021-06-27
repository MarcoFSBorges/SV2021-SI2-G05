using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IPlayerMapper : IMapper<Player, int?, List<Player>>
    {
        Login Update(Login login);
        Player Create(Player player);
        void CreateWithOptions(Login login, Item item, Clan clan);
        DataTable GetPlayerView();
        IList<Clan> GetClansOrClan(string clanName);
        Player Delete(Player player);
        List<Player> ReadAll();
        void Dispose();
        void OptimisticLocking(Login l);
    }
}
