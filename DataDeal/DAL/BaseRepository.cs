using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.DAL
{
    public class BaseRepository<T> where T :class      //where T:class对class进行参数约束，参数必须是class类型
    {
        private Models.DataDbContext db = ContextFactory.GetCurrentDbContext();
        public T AddEntity(T entity)
        {
            db.Entry(entity).State = EntityState.Added;
            return entity;//返回更改后的数据，例如数据库中的id
        }
        public bool UpdateEntity(T entity)
        {
            try
            {
                db.Set<T>().Attach(entity);
                db.Entry<T>(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception e)
            {
                //记录日志文件 e
                return false;
            }
        }
        public bool DeleteEntity(T entity)
        {
            try
            {
                db.Set<T>().Attach(entity);
                db.Entry<T>(entity).State = EntityState.Deleted;
                return true;
            }
            catch (Exception e)
            {
                //记录日志文件 e
                return false;
            }
        }
        public IQueryable<T> LoadEntities(Expression<Func<T,bool>> wherelamba)
        {
            try
            {
                return db.Set<T>().Where<T>(wherelamba).AsQueryable();
            }
            catch (Exception e)
            {
                //记录日志信息 e
                return null;
            }
            
        }
        public IQueryable<T> LoadPageEntities<S>(int pageIndex,int pageSize,out int total,Expression<Func<T,bool>> wherelamba,bool isAsc,Expression<Func<T,S>> orderBylamba)
        {
            try
            {
                var temp = db.Set<T>().Where<T>(wherelamba);
                total = temp.Count();
                if (isAsc)
                {
                    temp = temp.OrderBy<T, S>(orderBylamba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize).AsQueryable();
                }
                else
                {
                    temp = temp.OrderByDescending<T, S>(orderBylamba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize).AsQueryable();
                }
                return temp;
            }
            catch (Exception)
            {
                //记录出错信息，日志文件
                total = 0;
                return null;
            }
        }
    }
}
