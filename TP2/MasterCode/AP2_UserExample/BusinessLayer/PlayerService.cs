using DataLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Transactions;

namespace BusinessLayer
{
    public class PlayerService
    {
        protected SolutionType aSolutionType;
        protected IPlayerMapper aPlayerMapper;

        public PlayerService(SolutionType aSolutionType)
        {
            this.aSolutionType = aSolutionType;
            aPlayerMapper = MapperFactory.CreatePlayerMapper(aSolutionType);
        }

        public void CloseConnection()
        {
            aPlayerMapper.Dispose();
        }

        public IList<Player> ReadAll()
        {
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

        public DataTable PlayerView()
        {
            IPlayerMapper aPlayerMapper = MapperFactory.CreatePlayerMapper(aSolutionType);
            return aPlayerMapper.GetPlayerView();
        }

        public Player DeletePlayer(Player player)
        {
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
