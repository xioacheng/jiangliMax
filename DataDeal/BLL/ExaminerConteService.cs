using DataDeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.BLL
{
    public class ExaminerConteService:BaseService<M_ExaminerConte>
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DAL.RepositoryFactory.ExaminerConteRepository;
        }
    }
}
