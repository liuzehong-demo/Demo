using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Helper
{
    public class SQLHelper
    {
        public static string Main()
        {
            return "metadata=res://*/LZHData.csdl|res://*/LZHData.ssdl|res://*/LZHData.msl;provider=System.Data.SqlClient;provider connection string=\"data source=DESKTOP-LFHEM5N;initial catalog=LZH;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework\"";
        }
    }
}
