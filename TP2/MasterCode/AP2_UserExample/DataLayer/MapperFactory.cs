﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MapperFactory
    {

        public static IPlayerMapper createPlayerMapper(SolutionType theSolutionType) { 
            if(theSolutionType.Equals(SolutionType.ADO))
            {
                Context ctx = new Context();
                return new PlayerMapperAdo(ctx);
            }else if (theSolutionType.Equals(SolutionType.EF))
            {
                Context ctx = new Context();
                return new PlayerMapperEf(ctx);
            }
            throw new Exception("Apenas as implementações ADO.NET e EF são possíveis.");
        }
    }
}