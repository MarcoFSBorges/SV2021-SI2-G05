using System;

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
    }
}
