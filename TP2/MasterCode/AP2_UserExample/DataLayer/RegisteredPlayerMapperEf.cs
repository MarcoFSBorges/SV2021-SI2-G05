using EF;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class RegisteredPlayerMapperEf : IRegisteredPlayerMapper
    {
        protected Jogos_entities EFcontext;
        public readonly Context context;
        public RegisteredPlayerMapperEf(Context ctx)
        {
            context = ctx;
        }

        public RegisteredPlayer CreateWithOptions(RegisteredPlayer player, Item item, Clan clan)
        {
            throw new NotImplementedException();
        }

        public RegisteredPlayer Create(RegisteredPlayer entity)
        {
            throw new NotImplementedException();
        }

        public RegisteredPlayer Update(RegisteredPlayer entity)
        {
            throw new NotImplementedException();
        }

        public RegisteredPlayer Delete(RegisteredPlayer entity)
        {
            throw new NotImplementedException();
        }

        public RegisteredPlayer Read(int? id)
        {
            throw new NotImplementedException();
        }

        public List<RegisteredPlayer> ReadAll()
        {
            throw new NotImplementedException();
        }
    }
}
