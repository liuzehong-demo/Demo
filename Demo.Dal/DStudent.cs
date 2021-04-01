using Demo.EF;
using Demo.Model.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Dal
{
    public class DStudent
    {
        public Student GetModelByID(long ID)
        {
            Student model = null;
            using (var context = new LZHData())
            {
                model = context.Student.Find(ID);
            }
            return model;
        }
    }
}
