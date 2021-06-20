using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IMapper <T, Tid, Tcol>
    {
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
        T Read(Tid id);
        Tcol ReadAll();
    }
}
