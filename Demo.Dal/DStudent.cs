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
        private LZHData _Context = null;

        public DStudent() { }
        public DStudent(LZHData context)
        {
            _Context = context;
        }

        public bool Add(Student student)
        {
            var flag = false;
            if (_Context == null)
            {
                using (var context = new LZHData())
                {
                    context.Student.Add(student);
                    flag = context.SaveChanges() > 0 ;
                }
            } else
            {
                _Context.Student.Add(student);
                flag = _Context.SaveChanges() > 0;
            }
            return flag;
        }

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
