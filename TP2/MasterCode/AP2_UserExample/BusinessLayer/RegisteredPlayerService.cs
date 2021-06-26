using DataLayer;
using Model;
using System;
using System.Transactions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class RegisteredPlayerService
    {
        protected SolutionType aSolutionType;

        public RegisteredPlayerService(SolutionType aSolutionType)
        {
            this.aSolutionType = aSolutionType;
        }  

        public RegisteredPlayer CreatePlayerWithOptions(RegisteredPlayer player)
        {
            IRegisteredPlayerMapper aPlayerMapper = MapperFactory.createRegisteredPlayerMapper(aSolutionType);
            RegisteredPlayer createdPlayer = null;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    createdPlayer = aPlayerMapper.Create(player);
                    scope.Complete();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    scope.Dispose();
                }
            }
            return createdPlayer;
        }
    }
}
