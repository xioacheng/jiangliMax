using DataDeal.BLL;
using DataDeal.Models;
using DataDeal.Models.WxModels.Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Repository.WxRepository
{
    public class Comment
    {
        public static Comment comment = new Comment();
        public static Comment current {get { return comment; }}
        private CommentService dbcomment = new CommentService();
        public IEnumerable<WxCommentViewModel> GetCommentsByCaseId(int cid, int page)
        {
            int sum = 0;
            var list = m_getCommentsByCaseid(cid, page, out sum);
            List<WxCommentViewModel> result = new List<WxCommentViewModel>();
            foreach (var item in list)
            {
                result.Add(_transformviewmodel(item));
            }
            return result;
        }
        public IQueryable<M_Comment> m_getCommentsByCaseid(int cid, int page, out int sum)
        {
            return dbcomment.LoadPageEntities(page, 20, out sum, t => t.CaseId == cid, false, t => t.CaseId);
        }
        public M_Comment m_create(M_Comment comment)
        {
            CommentService dbcom = new CommentService();
            var result = dbcom.AddEntity(comment);
            return result;
        }
        #region 内部方法
        private WxCommentViewModel _transformviewmodel(M_Comment comment)
        {
            return new WxCommentViewModel()
            {
                caseid = comment.CaseId,
                content = comment.Content,
                created_at = comment.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                id = comment.ID,
                pid = "0",
                pusername = "achang",
                uid = comment.UserID,
                username = comment.UserName,
                avatar_url = UserInfo.Current.GetUserById(comment.UserID)!=null? UserInfo.Current.GetUserById(comment.UserID).Avatar_url:null,
            };
        }
        #endregion
    }
}
