//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Demo.Model.EFModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long SchoolId { get; set; }
    
        public virtual School School { get; set; }
    }
}
