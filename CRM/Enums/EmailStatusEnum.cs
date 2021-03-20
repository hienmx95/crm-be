using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class EmailStatusEnum
    {
        public static GenericEnum DONE = new GenericEnum { Id = 1, Code = "DONE", Name = "Đã gửi" };
        public static GenericEnum NOT_DONE = new GenericEnum { Id = 0, Code = "NOT_DONE", Name = "Chưa gửi" };

        public static List<GenericEnum> EmailStatusEnumList = new List<GenericEnum>()
        {
            DONE, NOT_DONE
        };
    }
}
