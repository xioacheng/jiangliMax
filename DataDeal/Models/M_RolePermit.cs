using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDeal.Models
{
    public class M_RolePermit
    {
        public int id { get; set; }
        public string RoleID { get; set; }
        public int PermitID { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyOn { get; set; }
    }
}
