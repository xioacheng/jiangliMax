using DataDeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.BLL
{
    public class InvolveService:BaseService<M_Involve>
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DAL.RepositoryFactory.InvolveRepository;
        }
    }
}
