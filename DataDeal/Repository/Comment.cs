using DataDeal.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Repository
{
    public class Comment
    {
        public int AddDiscuss(string username,string content,int caseid)
        {
            CommentService dbcomment = new CommentService();
            UserInfo dbuser = new UserInfo();
            string userid = dbuser.GetUseByName(username).Id;
            var result = dbcomment.AddEntity(new Models.M_Comment {
                CaseId = caseid,
                UserID = userid,
                Content = content,
                Date = DateTime.Now,
                UserName = username,
            });
            if (result.ID != 0)
            {
                return 1;
            }
            else return 0;
        }
    }
}
