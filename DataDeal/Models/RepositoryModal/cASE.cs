using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models.RepositoryModal
{
    public class UserCase
    {
        public IQueryable<M_Case> shenhelist { get; set; }
        public IQueryable<M_Case> fabulist { get; set; }
        public IQueryable<M_Case> shensulist { get; set; }
        public IQueryable<M_Case> wanchenglist { get; set; }
        public IQueryable<M_Case> caogaolist { get; set; }
    }
}
