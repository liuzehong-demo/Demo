using Demo.EF;
using Demo.Model.EFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Dal
{
    public class DPerson
    {
        private LZHData _Context = null;
        public DPerson() { }
        public DPerson(LZHData context)
        {
            _Context = context;
        }
        public bool Add(Person person)
        {
            var flag = false;
            if (_Context == null)
            {
                using (var context = new LZHData())
                {
                    context.Person.Add(person);
                    flag = context.SaveChanges() > 0;
                }
            } else
            {
                _Context.Person.Add(person);
                flag = _Context.SaveChanges() > 0 ;
            }
            return flag;
        }
    }
}
