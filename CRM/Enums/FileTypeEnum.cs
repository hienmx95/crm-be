using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class FileTypeEnum
    {
        public static GenericEnum CONTRACT = new GenericEnum { Id = 1, Code = "CONTRACT", Name = "Tập tài liệu hợp đồng" };
        public static GenericEnum COMPANY = new GenericEnum { Id = 2, Code = "COMPANY", Name = "Tập tài liệu của công ty" };
        public static List<GenericEnum> FileTypeEnumList = new List<GenericEnum>
        {
            CONTRACT, COMPANY
        };
    }
}
