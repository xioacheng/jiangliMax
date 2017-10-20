using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.DAL
{
    public interface IBaseRepository<T> where T :class
    {
        T AddEntity(T entity);
        bool UpdateEntity(T entity);
        bool DeleteEntity(T entity);
        IQueryable<T> LoadEntities(Expression<Func<T,bool>> wherelamba);
        IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> wherelamba, bool isAsc, Expression<Func<T, S>> orderBylambda);
    }
}
