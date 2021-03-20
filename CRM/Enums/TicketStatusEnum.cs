using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class TicketStatusEnum
    {
        public static GenericEnum NEW = new GenericEnum { Id = 1, Code = "NEW", Name = "Mới mở" };
        public static GenericEnum ASSIGNED = new GenericEnum { Id = 2, Code = "ASSIGNED", Name = "Đã tiếp nhận" };
        public static GenericEnum IN_PROGRESS = new GenericEnum { Id = 3, Code = "IN_PROGRESS", Name = "Đang xử lý" };
        public static GenericEnum WAITING_FOR_CUSTOMER = new GenericEnum { Id = 4, Code = "WAITING_FOR_CUSTOMER", Name = "Chờ thông tin KH" };
        public static GenericEnum WAITING_FOR_THIRD_PARTY = new GenericEnum { Id = 5, Code = "WAITING_FOR_THIRD_PARTY", Name = "Chờ thông tin bộ phận" };
        public static GenericEnum RESOLVED = new GenericEnum { Id = 6, Code = "RESOLVED", Name = "Đã xử lý" };
        public static GenericEnum CLOSED = new GenericEnum { Id = 7, Code = "CLOSED", Name = "Đóng" };
    }
}
