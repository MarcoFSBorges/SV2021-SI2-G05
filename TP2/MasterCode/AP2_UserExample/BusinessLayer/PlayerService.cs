using DataLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLayer
{
    public class PlayerService
    {
        protected SolutionType aSolutionType;

        public PlayerService(SolutionType aSolutionType)
        {
            this.aSolutionType = aSolutionType;
        }


        public IList<Player> readAll()
        {
            IPlayerMapper aPlayerMapper = MapperFactory.createPlayerMapper(aSolutionType);
            IList<Player> allPlayers = null;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    allPlayers = aPlayerMapper.ReadAll();
                    scope.Complete();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return allPlayers;
        }
        public Player createPlayer(Player player)
        {
            IPlayerMapper aPlayerMapper = MapperFactory.createPlayerMapper(aSolutionType);
            Player createdPlayer = null;
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
            }
            return createdPlayer;
        }


        public Login UpdatePlayer(Login login)
        {
            IPlayerMapper aPlayerMapper = MapperFactory.createPlayerMapper(aSolutionType);
            Login newLogin = null;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    newLogin = aPlayerMapper.Update(login);
                    scope.Complete();
                }
                catch (Exception e)
                {
                    throw e;
                }
                return newLogin;
            }
        }

        public void PlayerView()
        {
            IPlayerMapper aPlayerMapper = MapperFactory.createPlayerMapper(aSolutionType);
            aPlayerMapper.GetPlayerView();
        }

        public Player DeletePlayer(Player player)
        {
            IPlayerMapper aPlayerMapper = MapperFactory.createPlayerMapper(aSolutionType);
            Player createdPlayer = null;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    createdPlayer = aPlayerMapper.Delete(player);
                    scope.Complete();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return createdPlayer;
        }
    }
}
