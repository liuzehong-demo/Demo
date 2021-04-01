using Demo.Dal;
using Demo.Model.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Bll
{
    public class BStudent : BBase<BStudent>
    {
        readonly DStudent _dal = new DStudent();


        public Student GetModelByID(long ID)
        {
            return _dal.GetModelByID(ID);
        }
    }
}
