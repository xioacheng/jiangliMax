using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models
{
    //DbContext定义
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class DataDbContext:BaseDbContext
    {
        public DbSet<M_JiangliUser> jiangliuser { get; set; }
        public DbSet<M_JiangliRole> jianglirole { get; set; }
        public DbSet<M_Case> Case { get; set; }
        public DbSet<M_Appeal> Appeal { get; set; }
        public DbSet<M_Comment> Comment { get; set; }
        public DbSet<M_ExaminerConte> ExaminerConte { get; set; }
        public DbSet<M_Involve> Involve { get; set; }
        public DbSet<M_Permit> Permit { get; set; }
        public DbSet<M_RolePermit> RolePermit { get; set; }
        public DbSet<M_StationFeedBack> StationFeedBack { get; set; }
        public DbSet<M_FollowCase> FollowCase { get; set; }
        public static DataDbContext Create()
        {
            return new DataDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
