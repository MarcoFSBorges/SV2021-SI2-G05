using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IRegisteredPlayerMapper : IMapper<RegisteredPlayer, int?, List<RegisteredPlayer>>
    {
        RegisteredPlayer CreateWithOptions(RegisteredPlayer player, Item item, Clan clan);
    }
}
