using DataDeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.BLL
{
    public class StationFeedBackService:BaseService<M_StationFeedBack>
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DAL.RepositoryFactory.StationFeedBackRepository;
        }
    }
}
