using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public abstract class AbstractMapper<T, Tid, Tcol> : IMapper<T, Tid, Tcol> where T : class, new() where Tcol : IList<T>, IEnumerable<T>, new()
    {

        public readonly Context context;

        public AbstractMapper(Context ctx)
        {
            context = ctx;
        } 

        public T Create(T entity)
        {
            throw new NotImplementedException();
        }

        public T Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            /*
            using IDbCommand cmd = context.CreateCommand();
            cmd.CommandText = UpdateCommandText;
            cmd.CommandType = UpdateCommandType;
            UpdateParameters(cmd, entity);
            int result = cmd.ExecureNonQuery();
            return (result == 0) ? null : entity;
            */
            return null;
        }

        public void EnsureContext()
        {
            if (context == null)
            {
                throw new Exception("A contexto não foi bem inicializada...");
            }
        }

        public T Read(Tid id)
        {
            throw new NotImplementedException();
        }

        public Tcol ReadAll()
        {
            throw new NotImplementedException();
        }
    }
}
