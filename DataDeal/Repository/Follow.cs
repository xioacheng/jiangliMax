using DataDeal.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Repository
{
    public class Follow
    {
        /// <summary>
        /// 添加关注
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="caseid"></param>
        /// <returns></returns>
        public int addFollow(string  username,int caseid)
        {
            FollowCaseService dbfollow = new FollowCaseService();
            UserInfo dbuser = new UserInfo();
            string userid = dbuser.GetUseByName(username).Id;
            var followlist = dbfollow.LoadEntities(t=>t.caseid==caseid&&t.userid==userid);
            if (followlist.Count() > 0)
            {
                return -1;//不能重复关注
            }
            else
            {
                var addresult = dbfollow.AddEntity(new Models.M_FollowCase { userid=userid,caseid=caseid});
                if (addresult.id != 0)
                {
                    return dbfollow.LoadEntities(t=>t.caseid==caseid).Count();
                }
                else
                {
                    return -2;//上传数据库失败
                }
            }
        }
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="caseid"></param>
        /// <returns></returns>
        public int removeFollow(string username,int caseid)
        {
            FollowCaseService dbfollow = new FollowCaseService();
            UserInfo dbuser = new UserInfo();
            string userid = dbuser.GetUseByName(username).Id;
            var followlist = dbfollow.LoadEntities(t => t.caseid == caseid && t.userid == userid);
            if (followlist.Count() > 0)
            {
                var follow = followlist.FirstOrDefault();
                var removeResult = dbfollow.DeleteEntity(follow);
                if (removeResult)
                {
                    return dbfollow.LoadEntities(t => t.caseid==caseid).Count();//删除成功
                }
                else
                {
                    return -1;//删除关注失败
                }
            }
            else
            {
                return -2;//没有关注不能取消关注
            }
        }
    }
}
