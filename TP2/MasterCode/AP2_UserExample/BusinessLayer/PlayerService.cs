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

        public IList<Player> ReadAll()
        {
            IPlayerMapper aPlayerMapper = MapperFactory.CreatePlayerMapper(aSolutionType);
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
                finally
                {
                    scope.Dispose();
                }
            }
            return allPlayers;
        }

        public IList<Clan> GetClansOrClan(string clanName)
        {
            IPlayerMapper aPlayerMapper = MapperFactory.CreatePlayerMapper(aSolutionType);
            IList<Clan> clans = null;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    clans = aPlayerMapper.GetClansOrClan(clanName);
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
            return clans;
        }

        public Player CreatePlayer(Player player)
        {
            IPlayerMapper aPlayerMapper = MapperFactory.CreatePlayerMapper(aSolutionType);
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
                finally
                {
                    scope.Dispose();
                }
            }
            return createdPlayer;
        }

        public Login UpdatePlayer(Login login)
        {
            IPlayerMapper aPlayerMapper = MapperFactory.CreatePlayerMapper(aSolutionType);
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
                finally
                {
                    scope.Dispose();
                }
                return newLogin;
            }
        }

        public void PlayerView()
        {
            IPlayerMapper aPlayerMapper = MapperFactory.CreatePlayerMapper(aSolutionType);
            aPlayerMapper.GetPlayerView();
        }

        public Player DeletePlayer(Player player)
        {
            IPlayerMapper aPlayerMapper = MapperFactory.CreatePlayerMapper(aSolutionType);
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
