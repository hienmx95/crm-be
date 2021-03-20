using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class CurrentDepartmentEnum
    {
        public static GenericEnum ISNT = new GenericEnum { Id = 0, Code = "ISNT", Name = "Không phải phong ban áp dụng tài khoản hiện tại" };
        public static GenericEnum IS = new GenericEnum { Id = 1, Code = "IS", Name = "Là phòng ban áp dụng tài khoản hiện tại" };
        public static List<GenericEnum> CurrentDepartmentEnumList = new List<GenericEnum>
        {
            IS,ISNT,
        };
    }
}
