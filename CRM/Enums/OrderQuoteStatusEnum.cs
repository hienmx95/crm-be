using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class OrderQuoteStatusEnum
    {
        public static GenericEnum NEW = new GenericEnum { Id = 1, Code = "NEW", Name = "Mới tạo" };
        public static GenericEnum APPROVED = new GenericEnum { Id = 2, Code = "APPROVED", Name = "Đã duyệt" };
        public static GenericEnum SENT = new GenericEnum { Id = 3, Code = "SENT", Name = "Đã gửi" };

        public static List<GenericEnum> OrderQuoteStatusEnumList = new List<GenericEnum>()
        {
            NEW, APPROVED, SENT
        };
    }
}
