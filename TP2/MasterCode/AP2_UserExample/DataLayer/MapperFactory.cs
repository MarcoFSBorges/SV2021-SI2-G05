using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MapperFactory
    {
        public static IPlayerMapper CreatePlayerMapper(SolutionType theSolutionType)
        {
            Context ctx = new Context();
            if (theSolutionType.Equals(SolutionType.ADO))
            {
                return new PlayerMapperAdo(ctx);
            }
            else if (theSolutionType.Equals(SolutionType.EF))
            {
                return new PlayerMapperEf(ctx);
            }
            throw new Exception("Apenas as implementações ADO.NET e EF são possíveis.");
        }

        public static IRegisteredPlayerMapper CreateRegisteredPlayerMapper(SolutionType theSolutionType)
        {
            Context ctx = new Context();
            if (theSolutionType.Equals(SolutionType.ADO))
            {
                PlayerMapperAdo playerMapperAdo = new PlayerMapperAdo(ctx);
                return (IRegisteredPlayerMapper)playerMapperAdo;
            }
            else if (theSolutionType.Equals(SolutionType.EF))
            {
                PlayerMapperEf playerMapperEf = new PlayerMapperEf(ctx);
                return (IRegisteredPlayerMapper)playerMapperEf;
            }
            throw new Exception("Apenas as implementações ADO.NET e EF são possíveis.");
        }
    }
}
