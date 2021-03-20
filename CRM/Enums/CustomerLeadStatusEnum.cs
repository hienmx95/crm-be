using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class CustomerLeadStatusEnum
    {
        public static GenericEnum NEW = new GenericEnum { Id = 1, Code = "NEW", Name = "Mới tạo" };
        public static GenericEnum SKIP = new GenericEnum { Id = 2, Code = "SKIP", Name = "Không quan tâm" };
        public static GenericEnum CARE = new GenericEnum { Id = 3, Code = "CARE", Name = "Cần quan tâm" };
        public static GenericEnum KEEPACONTACT = new GenericEnum { Id = 4, Code = "KEEPACONTACT", Name = "Cố liên lạc" };
        public static GenericEnum INPROGRESS = new GenericEnum { Id = 5, Code = "INPROGRESS", Name = "Đang chăm sóc" };
        public static GenericEnum DONE = new GenericEnum { Id = 6, Code = "DONE", Name = "Đã chuyển đổi" };

        public static List<GenericEnum> CustomerLeadStatusEnumList = new List<GenericEnum>
        {
            NEW, SKIP, CARE, KEEPACONTACT, INPROGRESS, DONE
        };
    }

}
