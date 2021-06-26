using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class PlayerMapperEf : AbstractMapper<Player, int?, List<Player>>, IPlayerMapper
    {
        public PlayerMapperEf(Context ctx) : base(ctx)
        {
        }

        public new Player Create(Player entity)
        {
            throw new NotImplementedException();
        }

        public new Player Delete(Player entity)
        {
            throw new NotImplementedException();
        }

        public IList<Clan> GetClansOrClan(string clanName)
        {
            throw new NotImplementedException();
        }

        public void GetPlayerView()
        {
            throw new NotImplementedException();
        }

        public new Player Read(int? id)
        {
            throw new NotImplementedException();
        }

        public new List<Player> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Login Update(Login login)
        {
            throw new NotImplementedException();
        }
    }
}
