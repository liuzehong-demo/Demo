using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Bll
{
    public class BBase<T> where T : new()
    {
        public static T Instance = new T();
     
    }
}
