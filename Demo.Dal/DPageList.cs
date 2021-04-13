using Demo.EF;
using Demo.Model.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Dal
{
    public class DPageList
    {
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="pageSize">页码</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="countNum">总数</param>
        /// <returns></returns>
        public IList<Student> GetList(int pageSize, int pageIndex, ref long countNum)
        {
            IList<Student> list = new List<Student>();

            using (var context = new LZHData())
            {
                var query = context.Student.OrderBy(x => x.ID).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
                query = query.Where(x => x.ID > 10);


                list = query.ToList();

                countNum = context.Student.Count();

            }
            return list;
        }
    }
}
