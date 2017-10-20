using DataDeal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.BLL
{
    public abstract class BaseService<T> where T :class
    {
        protected IBaseRepository<T> CurrentRepository { get; set; }
        public BaseService() { SetCurrentRepository(); }
        public abstract void SetCurrentRepository();
        public T AddEntity(T entity)
        {
            var addentity = CurrentRepository.AddEntity(entity);
            DbSession.SaveChanges();
            return addentity;
        }
        public bool UpdateEntity(T entity)
        {
            CurrentRepository.UpdateEntity(entity);
            return DbSession.SaveChanges() > 0;
        }
        public bool DeleteEntity(T entity)
        {
            CurrentRepository.DeleteEntity(entity);
            return DbSession.SaveChanges() > 0 ;
        }
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> wherelambda)
        {
            return CurrentRepository.LoadEntities(wherelambda);
        }
        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> wherelambda, bool isAsc, Expression<Func<T, S>> orderBylambda)
        {
            return CurrentRepository.LoadPageEntities<S>(pageIndex,pageSize,out total,wherelambda,isAsc,orderBylambda);
        }
    }
}
