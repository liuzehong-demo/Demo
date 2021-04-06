using Demo.Dal;
using Demo.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Model.EFModel;

namespace Demo.Bll
{
    public class BTransaction : BBase<BTransaction>
    {
        public void Run()
        {
            new DStudent().Add(new Student()
            {
                Name = "这不是一个事务"
            });
            using (var context = new LZHData())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    DPerson dPerson = new DPerson(context);
                    DStudent dStudent = new DStudent(context);

                    dPerson.Add(new Person() { Age = 1, FirstName = "f", LastName = "f" });

                    dStudent.Add(new Student() { Name = "f" });

                    dbContextTransaction.Commit();
                }
            }
        }
    }
}
