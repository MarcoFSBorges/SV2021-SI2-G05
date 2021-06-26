using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class RegisteredPlayerMapperEf : AbstractMapper<RegisteredPlayer, int?, List<RegisteredPlayer>>, IRegisteredPlayerMapper
    {
        public RegisteredPlayerMapperEf(Context ctx) : base(ctx)
        {
        }

        public RegisteredPlayer CreateWithOptions(RegisteredPlayer player, Item item, Clan clan)
        {
            throw new NotImplementedException();
        }
    }
}
