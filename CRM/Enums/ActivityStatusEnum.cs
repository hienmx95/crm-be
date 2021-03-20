using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class ActivityStatusEnum
    {
        public static GenericEnum NEW = new GenericEnum { Id = 1, Code = "NEW", Name = "Mới" };
        public static GenericEnum PLAN = new GenericEnum { Id = 2, Code = "PLAN", Name = "Lên kế hoạch" };
        public static GenericEnum PROCESS = new GenericEnum { Id = 3, Code = "PROCESS", Name = "Đang xử lý" };
        public static GenericEnum DONE = new GenericEnum { Id = 4, Code = "DONE", Name = "Đã hoàn thành" };

        public static List<GenericEnum> ActivityStatusEnumList = new List<GenericEnum>()
        {
            NEW, PLAN, PROCESS, DONE
        };
    }

}
