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

        DataTable GetPlayerView();
        IList<Clan> GetClansOrClan(string clanName);

        void Dispose();
        void OptimisticLocking(Login l);
    }
}
