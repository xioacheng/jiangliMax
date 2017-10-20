using DataDeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.BLL
{
    public class FollowCaseService:BaseService<M_FollowCase>
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DAL.RepositoryFactory.FollowCaseRepository;
        }
    }
}
