using Demo.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Model.EFModel;
using Newtonsoft.Json;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //BTransaction.Instance.Run();
            Student student = new Student()
            {
                ID = 1
            };
            var jsonData = JsonConvert.SerializeObject(student);
            Console.WriteLine(jsonData);
            Console.ReadKey();
        }
    }
}
