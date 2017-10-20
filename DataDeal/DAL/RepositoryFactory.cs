using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.DAL
{
    /// <summary>
    /// 获取模型类的实体类型
    /// </summary>
    public static class RepositoryFactory
    {
        public static CaseRepository CaseRepository {get { return new CaseRepository(); }}
        public static AppealRepository AppealRepository { get { return new AppealRepository(); } }
        public static CommentRepository CommentRepository { get { return new CommentRepository(); } }
        public static ExaminerConteRepository ExaminerConteRepository { get { return new ExaminerConteRepository(); } }
        public static InvolveRepository InvolveRepository { get { return new InvolveRepository(); } }
        public static PermitRepository PermitRepository { get { return new PermitRepository(); } }
        public static RolePermitRepository RolePermitRepository { get { return new RolePermitRepository(); } }
        public static StationFeenBackRepository StationFeedBackRepository { get { return new StationFeenBackRepository(); } }
        public static FollowCaseRepository FollowCaseRepository { get { return new FollowCaseRepository(); } }
    }
}
