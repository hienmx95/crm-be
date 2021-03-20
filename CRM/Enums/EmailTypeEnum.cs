using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class EmailTypeEnum
    {
        public static GenericEnum PERSONAL = new GenericEnum { Id = 1, Code = "PERSONAL", Name = "Cá nhân" };
        public static GenericEnum COMPANY = new GenericEnum { Id = 0, Code = "NOT_DONE", Name = "Công ty" };

        public static List<GenericEnum> EmailTypeEnumList = new List<GenericEnum>()
        {
            PERSONAL, COMPANY
        };
    }
}
